/*
 * Copyright (c) 2008-2010 by Joel Uckelman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License (LGPL) as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, copies are available
 * at http://www.opensource.org.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Info = VassalSharp.Info;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using ExtensionMetaData = VassalSharp.build.module.metadata.ExtensionMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using SaveMetaData = VassalSharp.build.module.metadata.SaveMetaData;
using Resources = VassalSharp.i18n.Resources;

namespace VassalSharp.launch
{
    /// <summary>
    /// Encapsulates and parses command-line arguments.
    /// <code>args</code> and <code>LaunchRequest.parseArgs(args).toArgs()</code>
    /// are equivalent (though perhaps not equal) argument lists.
    /// </summary>
    /// <author>Joel Uckelman</author>
    /// <since>3.1.0</since>
    /// 
    [Serializable]
    public class LaunchRequest
    {

        public class OptDummy
        {
            private string v1;
            private string[] args;
            private string v2;
            private int longOpts;

            public OptDummy(string v1, string[] args, string v2, int longOpts)
            {
                this.v1 = v1;
                this.args = args;
                this.v2 = v2;
                this.longOpts = longOpts;
            }

            internal int getopt()
            {
                throw new NotImplementedException();
            }

            internal string getOptarg()
            {
                throw new NotImplementedException();
            }

            internal int getOptind()
            {
                throw new NotImplementedException();
            }

            internal char getOptopt()
            {
                throw new NotImplementedException();
            }
        }


        public enum Mode
        {
            MANAGE,
            LOAD,
            EDIT,
            IMPORT,
            NEW,
            EDIT_EXT,
            NEW_EXT,
            TRANSLATE
        }

        public Mode mode;

        public FileInfo module;
        public FileInfo game;
        public FileInfo extension;
        public FileInfo importFile;

        public bool standalone = false;

        public bool builtInModule;
        public List<String> autoext;

        public int port = -1;

        public long key;

        public LaunchRequest() : this(default(Mode), null)
        {
        }

        public LaunchRequest(Mode mode) : this(mode, null, null)
        {
        }

        public LaunchRequest(Mode mode, FileInfo module) : this(mode, module, null)
        {
        }

        public LaunchRequest(Mode mode, FileInfo module, FileInfo other)
        {
            this.mode = mode;
            this.module = module;


            if (mode == Mode.EDIT_EXT) extension = other;

            else game = other;

        }


        public LaunchRequest(LaunchRequest lr)

        {
            this.mode = lr.mode;

            this.module = lr.module;

            this.game = lr.game;

            this.extension = lr.extension;

            this.importFile = lr.importFile;


            this.standalone = lr.standalone;


            this.builtInModule = lr.builtInModule;


            if (lr.autoext != null) this.autoext = new List<string>(lr.autoext);

        }

        /// <summary> Create an argument array equivalent to this <code>LaunchRequest</code>.
        /// 
        /// </summary>
        /// <returns> an array which would be parsed to this <code>LaunchRequest</code>
        /// </returns>

        public string[] toArgs()

        {
            List<string> args = new List<string>();

            args.Add("--+mode".ToString());

            if (builtInModule) args.Add("--auto");

            if (port >= 0) args.Add("--port = +port");

            if (autoext != null)
            {
                StringBuilder sb = new StringBuilder("--auto - extensions =");
                IEnumerator<String> i = autoext.GetEnumerator();
                sb.Append(i.Current);
                while (i.MoveNext()) sb.Append(",").Append(i.Current);
                args.Add(sb.ToString().Replace(" ", "_"));
            }

            args.Add("--");

            if (module != null)
            {
                args.Add(module.FullName);
                if (game != null)
                {
                    args.Add(game.FullName);
                }
                else if (extension != null)
                {
                    args.Add(extension.FullName);
                }
            }
            else if (importFile != null)
            {
                args.Add(importFile.FullName);
            }

            return args.ToArray();
        }

        // FIXME: translate this somehow?
        private static String help =
        "Usage:\n" +
          "VASSAL -e [option]... module\n" +
          "VASSAL -i [option]... module\n" +
          "VASSAL -l [option]... module|save|log...\n" +
          "VASSAL -n [option]...\n" +
          "VASSAL -m\n" +
          "VASSAL -h\n" +
          "VASSAL --edit-extension [option]... module|extension...\n" +
          "VASSAL --new-extension [option]...\n" +
        "\n" +
        "Options:\n" +
          "-a, --auto          TODO\n" +
          "-e, --edit          Edit a module\n" +
          "-h, --help          Display this help and exit\n" +
          "-i, --import        Import a non-VASSAL module\n" +
          "-l, --load          Load a module and saved game or log\n" +
          "-m, --manage        Use the module manager\n" +
          "-n, --new           Create a new module\n" +
          "-s, --standalone    Run in standalone mode\n" +
          "--auto-extensions   TODO\n" +
          "--edit-extension    Edit a module extension\n" +
          "--new-extension     Create a new module extension\n" +
          "--port              Set port for manager to listen on\n" +
          "--version           Display version information and exit\n" +
          "--                  Terminate the list of options\n" +
        "\n" +
        "VASSAL defaults to '-m' if no options are given.\n" +
        "\n";

        /// <summary> Parse an argument array to a <code>LaunchRequest</code>.
        /// 
        /// </summary>
        /// <param name="args">an array of command-line arguments
        /// </param>
        /// <returns> a <code>LaunchRequest</code> equivalent to <code>args</code>
        /// </returns>
        /// <throws>  LaunchRequestException when parsing fails </throws>
        public static LaunchRequest parseArgs(String[] args)
        {
            LaunchRequest lr = new LaunchRequest();

            const int AUTO_EXT = 2;
            const int EDIT_EXT = 3;
            const int NEW_EXT = 4;
            const int PORT = 5;
            const int VERSION = 6;
            const int TRANSLATE = 7;

            //LongOpt [] longOpts = new LongOpt []
            //{ 
            //	new LongOpt(auto, LongOpt.NO_ARGUMENT, null, a), 
            //	new LongOpt(edit, LongOpt.NO_ARGUMENT, null, e), 
            //	new LongOpt(help, LongOpt.NO_ARGUMENT, null, h), 
            //	new LongOpt(import, LongOpt.NO_ARGUMENT, null, i), 
            //	new LongOpt(load, LongOpt.NO_ARGUMENT, null, l), 
            //	new LongOpt(manage, LongOpt.NO_ARGUMENT, null, m), 
            //	new LongOpt(new, LongOpt.NO_ARGUMENT, null, n), 
            //	new LongOpt(standalone, LongOpt.NO_ARGUMENT, null, s), 
            //	new LongOpt(auto-extensions, LongOpt.REQUIRED_ARGUMENT, null, AUTO_EXT), 
            //	new LongOpt(edit-extension, LongOpt.NO_ARGUMENT, null, EDIT_EXT), 
            //	new LongOpt(new-extension, LongOpt.NO_ARGUMENT, null, NEW_EXT), 
            //	new LongOpt(port, LongOpt.REQUIRED_ARGUMENT, null, PORT), 
            //	new LongOpt(version, LongOpt.NO_ARGUMENT, null, VERSION), 
            //	new LongOpt(translate, LongOpt.NO_ARGUMENT, null, TRANSLATE)
            //};

            //Getopt g = new Getopt(VASSAL, args, :aehilmn, longOpts);
            int longOpts = 0;
            OptDummy g = new OptDummy("VASSAL", args, ":aehilmn", longOpts);
            //g.setOpterr(false);

            int c;
            while ((c = g.getopt()) != -1)
            {
                switch (c)
                {
                    case AUTO_EXT:
                        if (lr.autoext == null) lr.autoext = new List<String>();
                        foreach (String ext in g.getOptarg().Split(','))
                        {
                            lr.autoext.Add(ext.Replace("_", " "));
                        }
                        break;

                    case EDIT_EXT:
                        setMode(lr, Mode.EDIT_EXT);
                        break;

                    case NEW_EXT:
                        setMode(lr, Mode.NEW_EXT);
                        break;

                    case PORT:
                        try
                        {
                            lr.port = int.Parse(g.getOptarg());
                        }
                        catch (FormatException e)
                        {
                            die("LaunchRequest.bad_port", g.getOptarg());

                        }

                        if (lr.port < 49152 || lr.port > 65535)

                        {
                            die("LaunchRequest.bad_port", g.getOptarg());

                        }
                        break;

                    case VERSION:
                        System.Windows.Forms.MessageBox.Show("VASSAL" + Info.Version, "Info", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        System.Windows.Forms.Application.Exit();
                        break;

                    case TRANSLATE:
                        setMode(lr, Mode.TRANSLATE);
                        break;

                    case 'a':
                        lr.builtInModule = true;
                        break;

                    case 'e':
                        setMode(lr, Mode.EDIT);
                        break;

                    case 'h':
                        System.Windows.Forms.MessageBox.Show(help, "Help", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Question);
                        System.Windows.Forms.Application.Exit();
                        break;

                    case 'i':
                        setMode(lr, Mode.IMPORT);
                        break;

                    case 'l':
                        setMode(lr, Mode.LOAD);
                        break;

                    case 'm':
                        setMode(lr, Mode.MANAGE);
                        break;

                    case 'n':
                        setMode(lr, Mode.NEW);
                        break;

                    case 's':
                        lr.standalone = true;
                        break;

                    case ':':
                        die("LaunchRequest.missing_argument", args[g.getOptind() - 1]);
                        break;

                    case '?':
                        // NB: getOptind() is not advanced if the unrecognized option
                        // is short and bundled, so we must handle unrecognized long
                        // options separately from unrecognized short options.
                        die(
                        "LaunchRequest.unrecognized_option",
                        g.getOptopt() == 0 ? args[g.getOptind() - 1] : "-" + new string((char)g.getOptopt(), 1));
                        break;

                    default:
                        // should never happen
                        throw new ApplicationException();
                }
            }

            int i = g.getOptind();

            // load by default if a non-option argument is given; otherwise, manage

            if (lr.mode == null)
            {
                lr.mode = i < args.Length ? Mode.LOAD : Mode.MANAGE;

            }

            // get the module and game, if specified

            switch (lr.mode)
            {

                case Mode.MANAGE:
                    break;

                case Mode.LOAD:
                    while (i < args.Length)
                    {
                        FileInfo file = new FileInfo(args[i++]);

                        AbstractMetaData data = MetaDataFactory.buildMetaData(file);

                        if (data is ModuleMetaData)

                        {
                            if (lr.module != null)
                                die("LaunchRequest.only_one", "module");

                            lr.module = file;

                        }
                        else if (data is ExtensionMetaData)

                        {
                            if (lr.extension != null) die(string.Empty);

                            lr.extension = file;

                        }
                        else if (data is SaveMetaData)

                        {
                            if (lr.game != null)
                                die("LaunchRequest.only_one", "saved game or log");

                            lr.game = file;

                        }
                        else

                        {
                            die("LaunchRequest.unknown_file_type", file.ToString());

                        }

                    }

                    if (!lr.builtInModule && lr.module == null && lr.game == null)
                    {
                        die("LaunchRequest.missing_module");

                    }
                    break;

                case Mode.IMPORT:
                    if (i < args.Length)
                    {
                        lr.importFile = new FileInfo(args[i++]);

                    }
                    else
                    {
                        die("LaunchRequest.missing_module");

                    }
                    break;

                case Mode.EDIT:

                case Mode.NEW_EXT:
                    if (i < args.Length)
                    {
                        FileInfo file = new FileInfo(args[i++]);

                        AbstractMetaData data = MetaDataFactory.buildMetaData(file);

                        if (data is ModuleMetaData)

                        {
                            lr.module = file;

                        }
                        else if (data is ExtensionMetaData)

                        {

                        }
                        else if (data is SaveMetaData)

                        {

                        }
                        else

                        {
                            die("LaunchRequest.unknown_file_type", file.ToString());

                        }

                    }
                    else
                    {
                        die("LaunchRequest.missing_module");

                    }
                    break;

                case Mode.EDIT_EXT:
                    while (i < args.Length)
                    {
                        FileInfo file = new FileInfo(args[i++]);
                        AbstractMetaData data = MetaDataFactory.buildMetaData(file);

                        if (data is ModuleMetaData)
                        {
                            if (lr.module != null)
                                die("LaunchRequest.only_one", "module");
                            lr.module = file;
                        }
                        else if (data is ExtensionMetaData)
                        {
                            if (lr.extension != null) die(string.Empty);
                            lr.extension = file;
                        }
                        else if (data is SaveMetaData)
                        {
                        }
                        else
                        {
                            die("LaunchRequest.unknown_file_type", file.ToString());
                        }

                    }

                    if (lr.module == null)
                    {
                        die("LaunchRequest.missing_module");
                    }

                    if (lr.extension == null)
                    {
                        die("LaunchRequest.missing_extension");
                    }
                    break;

                case Mode.NEW:

                case Mode.TRANSLATE:
                    break;

            }

            if (i < args.Length)
            {
                die("LaunchRequest.excess_args", args[i]);
            }

            // other consistency checks

            if (lr.builtInModule)
            {
                if (lr.mode != Mode.LOAD)
                {
                    die("LaunchRequest.only_in_mode, --auto", Mode.LOAD.ToString());
                }

                if (lr.module != null)
                {
                    die("LaunchRequest.excess_args", args[i]);
                }
            }

            if (lr.autoext != null)
            {
                if (lr.mode != Mode.LOAD)
                {
                    die("LaunchRequest.only_in_mode",
                    "--auto - extensions", Mode.LOAD.ToString());

                }

                if (lr.module != null)
                {
                    die("LaunchRequest.excess_args", args[i]);

                }

            }

            return lr;

        }


        protected static void setMode(LaunchRequest lr, Mode mode)
        {
            if (lr.mode != null) die("LaunchRequest.only_one", mode.ToString());
            lr.mode = mode;
        }

        /// <summary> Throws a {@link LaunchRequestException}.
        /// 
        /// </summary>
        /// <param name="key">{@link Resources} key
        /// </param>
        /// <param name="vals">{@link Resources} arguments
        /// </param>
        /// <throws>  LaunchRequestException always </throws>

        protected static void die(String key, params string[] vals)
        {
            throw new LaunchRequestException(key, vals);
        }

    }
}
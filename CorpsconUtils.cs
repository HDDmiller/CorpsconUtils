using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;


namespace YourNameSpace
{
    class  CorpsconUtils
    {
        private static string CorpsconPath = @Trenchless_Designer.Properties.Settings.Default.CorpsconPath;
        private static string NadconPath = CorpsconPath + @"Nadcon\";
		private static string VertconPath = CorpsconPath + @"Vercon\";
		private static string GeoidPath = CorpsconPath + @"Geoid\";

		

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

        //Define all of the delagates for the Corpscon functions.
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int corpscon_default_config();

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetNadconPath(string path);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetVertconPath(string path);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetGeoidPath(string path);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int SetInSystem(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutSystem(int val);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int GetInSystem();		

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int GetOutSystem();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetInDatum(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutDatum(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetInUnits(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutUnits(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetInZone(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutZone(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetInVDatum(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutVDatum(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetInVUnits(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutVUnits(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetInHPGNArea(string area);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetOutHPGNArea(string area);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetUseVertconCustomAreas(int option);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetGeoidCodeBase(int val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetUseGeoidCustomAreas(int option);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int corpscon_initialize_convert();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetXIn(double val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetYIn(double val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int SetZIn(double val);

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int corpscon_convert();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate double GetXOut();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate double GetYOut();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate double GetZOut();

		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate int corpscon_clean_up();
				
        //Configures the Corpscon .dll and sets the paths for the Nadcon, Vertcon and Geoid directories.
        public static int Initialize()
        {
            int configured =0;
            
            IntPtr  pDll = LoadLibrary(CorpsconPath  + "corpscon_v6.dll");
            IntPtr pAddressToCall = GetProcAddress(pDll, "corpscon_default_config");

            corpscon_default_config configure_corpscon = (corpscon_default_config)Marshal.GetDelegateForFunctionPointer(pAddressToCall,typeof(corpscon_default_config));

            configured = configure_corpscon();           
                  
            if (configured == 1)
            {
				pAddressToCall = GetProcAddress(pDll, "SetNadconPath");
                SetNadconPath setnadconpath = (SetNadconPath)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetNadconPath));
                int nadconset = setnadconpath(NadconPath);

                pAddressToCall = GetProcAddress(pDll, "SetVertconPath");
                SetVertconPath setvertconpath = (SetVertconPath)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetVertconPath));
                int vertconset = setvertconpath(VertconPath);

                pAddressToCall = GetProcAddress(pDll, "SetGeoidPath");
                SetGeoidPath setgeoidpath = (SetGeoidPath)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetGeoidPath));
                int geoidset = setgeoidpath(GeoidPath);                
            }
            return configured;
        }


		//Set the coordinate system of the input coordinates
        public static int SetInputSystem(int value)
        {
            IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
            IntPtr pAddressToCall = GetProcAddress(pDll, "SetInSystem");

            SetInSystem setinsystem = (SetInSystem)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetInSystem));

            int confirm = setinsystem(value);
            return confirm;
        }

		//Set the coordinate system of the output coordinates
		public static int SetOutputSystem(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetOutSystem");

			SetOutSystem setoutsystem = (SetOutSystem)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetOutSystem));

			int confirm = setoutsystem(value);
			return confirm;
		}

		//Set the datum of the input coordinates
		public static int SetInputDatum(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetInDatum");

			SetInDatum setindatum = (SetInDatum)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetInDatum));

			int confirm = setindatum(value);
			return confirm;
		}

		//Set the datum of the output coordinates
		public static int SetOutputDatum(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetOutDatum");

			SetOutDatum setoutdatum = (SetOutDatum)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetOutDatum));

			int confirm = setoutdatum(value);
			return confirm;
		}

		//Set the units of the input coordinates
		public static int SetInputUnits(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetInUnits");

			SetInUnits setinunits = (SetInUnits)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetInUnits));

			int confirm = setinunits(value);
			return confirm;
		}

		//Set the units of the output coordinates
		public static int SetOutputUnits(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetOutUnits");

			SetOutUnits setoutunits = (SetOutUnits)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetOutUnits));

			int confirm = setoutunits(value);
			return confirm;
		}

		//Set the zone of the input coordinates
		public static int SetInputZone(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetInZone");

			SetInZone setinzone = (SetInZone)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetInZone));

			int confirm = setinzone(value);
			return confirm;
		}

		//Set the zone of the output coordinates
		public static int SetOutputZone(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetOutZone");

			SetOutZone setoutzone = (SetOutZone)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetOutZone));

			int confirm = setoutzone(value);
			return confirm;
		}

		//Set the vertical datum of the input coordinates
		public static int SetInputVDatum(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetInVDatum");

			SetInVDatum setinvdatum = (SetInVDatum)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetInVDatum));

			int confirm = setinvdatum(value);
			return confirm;
		}

		//Set the vertical datum of the output coordinates
		public static int SetOutputVDatum(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetOutVDatum");

			SetOutVDatum setoutvdatum = (SetOutVDatum)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetOutVDatum));

			int confirm = setoutvdatum(value);
			return confirm;
		}

		//Set the vertical units of the input coordinates.
		public static int SetInputVUnits(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetInVUnits");

			SetInVUnits setinvunits = (SetInVUnits)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetInVUnits));

			int confirm = setinvunits(value);
			return confirm;
		}

		//Set the vertical units of the output coordinates.
		public static int SetOutputVUnits(int value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetOutVUnits");

			SetOutVUnits setoutvunits = (SetOutVUnits)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetOutVUnits));

			int confirm = setoutvunits(value);
			return confirm;
		}

		//Set the Use Custom Vertcon Areas.
		public static int SetUseVertconCustom(int opt)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetUseVertconCustomAreas");

			SetUseVertconCustomAreas setvconcustom = (SetUseVertconCustomAreas)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetUseVertconCustomAreas));

			int confirm = setvconcustom(opt);
			return confirm;
		}

		//Set the Geoid Code Base.
		public static int SetGeoid(int val)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetGeoidCodeBase");

			SetGeoidCodeBase setgeoidcodebase = (SetGeoidCodeBase)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetGeoidCodeBase));

			int confirm = setgeoidcodebase(val);
			return confirm;
		}

		//Set the Use Custom Geoid Areas.
		public static int SetUseGeoidCustom(int opt)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetUseGeoidCustomAreas");

			SetUseGeoidCustomAreas setgeoidcustom = (SetUseGeoidCustomAreas)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetUseGeoidCustomAreas));

			int confirm = setgeoidcustom(opt);
			return confirm;
		}

		//Initialize Corpscon for conversion.
		public static int CorpsconInitializeConvert()
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "corpscon_initialize_convert");

			corpscon_initialize_convert corpscon_init_convert = (corpscon_initialize_convert)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(corpscon_initialize_convert));

			int confirm = corpscon_init_convert();
			return confirm;
		}

		//Set the input X coordinate.
		public static int SetXInput(double  value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetXIn");

			SetXIn setxin = (SetXIn)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetXIn));

			int confirm = setxin(value);
			return confirm;
		}

		//Set the input Y coordinate.
		public static int SetYInput(double value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetYIn");

			SetYIn setyin = (SetYIn)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetYIn));

			int confirm = setyin(value);
			return confirm;
		}

		//Set the input Z coordinate.
		public static int SetZInput(double value)
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "SetZIn");

			SetZIn setzin = (SetZIn)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(SetZIn));

			int confirm = setzin(value);
			return confirm;
		}


		//Perform the conversion.
		public static int CorpsconConvert()
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "corpscon_convert");

			corpscon_convert corpscon_convert = (corpscon_convert)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(corpscon_convert));

			int confirm = corpscon_convert();
			return confirm;
		}


		//Get the converted X Coordinate.
		public static double GetXOutput()
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "GetXOut");

			GetXOut getxout = (GetXOut)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(GetXOut));

			double XCoord = getxout();
			return XCoord;
		}

		//Get the converted Y Coordinate.
		public static double GetYOutput()
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "GetYOut");

			GetYOut getyout = (GetYOut)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(GetYOut));

			double YCoord = getyout();
			return YCoord;
		}

		//Get the converted Z Coordinate.
		public static double GetZOutput()
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "GetZOut");

			GetZOut getzout = (GetZOut)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(GetZOut));

			double ZCoord = getzout();
			return ZCoord;
		}

		//Clean up after the conversion.
		public static int clean_up()
		{
			IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
			IntPtr pAddressToCall = GetProcAddress(pDll, "corpscon_clean_up");

			corpscon_clean_up cleanup = (corpscon_clean_up)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(corpscon_clean_up));

			int result = cleanup();
			return result;
		}



		//Get the coordinate system setting from the .dll
        public static int GetInputSystem()
        {
            IntPtr pDll = LoadLibrary(CorpsconPath + "corpscon_v6.dll");
            IntPtr pAddressToCall = GetProcAddress(pDll, "GetInSystem");

            GetInSystem getinsystem = (GetInSystem)Marshal.GetDelegateForFunctionPointer(pAddressToCall, typeof(GetInSystem));

            int result = getinsystem();
            return result;
        }



    }
}
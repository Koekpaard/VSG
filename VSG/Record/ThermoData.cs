namespace VSG.Record
{
    internal record ThermoData
    {
        public int Dd { get; set; }
        public int Max { get; set; }    // *
        public int Min { get; set; }    // *
        public int Avg { get; set; }
        public int? Dxc { get; set; }
        public double AvDR { get; set; }
        public string OHrA // Geen idee wat dit is en ken daarom het belang er niet van. Dit is behoorlijk lomp, maar wanneer het een waarde heeft wordt dit in elk geval opgemerkt.
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
        public float TPccn { get; set; }
        public WvTupeEnum WvTupe { get; set; }
        public string XDir { get; set; }
        public double AvSt { get; set; }
        public string Dir { get; set; }
        /// <summary>De 13e kolom</summary>
        public int MxD1 { get; set; }    // *
        public double SkyT { get; set; }
        /// <summary>De 15e kolom</summary>
        public int MxD2 { get; set; }
        public int MnD { get; set; }
        public double AtSRP { get; set; }

        internal enum WvTupeEnum
        {
            R = 1,
            T = 2,
            F = 4,
            Z = 8
        }
    }
}

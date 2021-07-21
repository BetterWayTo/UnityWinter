namespace Winter
{
    public class Property
    {
        public string Name { get => m_Name; set => m_Name = value; }
        public string Value { get => m_Value; set => m_Value = value; }
        public string Bean { get => m_Bean; set => m_Bean = value; }

        private string m_Name;
        private string m_Value;
        private string m_Bean;
    }
}
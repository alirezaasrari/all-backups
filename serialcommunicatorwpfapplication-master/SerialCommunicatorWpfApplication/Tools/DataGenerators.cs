namespace SerialCommunicatorWpfApplication.Tools
{
    public static class DataGenerators
    {
        public static string DataGenerator(string cmdcode, string? codesetting1, string? codesetting2
            , string? codesetting3, string? codesetting4, string? codesetting5, string? codesetting6
            , string? codesetting7, string? codesetting8, string? codesetting9, string? codesetting10
            , string? codesetting11, string? codesetting12)
        {
            if (cmdcode == "01")
            {
                return "";
            }
            else if(cmdcode == "02")
            {
                return "";
            }
            else if (cmdcode == "03")
            {
                var data = "";
                var translateddata = StringToHexConvertor.StringToHex(data);
                return translateddata;
            }
            else if(cmdcode == "04")
            {
                var data1 = codesetting1.Length > 0 ? $"100:{codesetting1}," : "";
                var data2 = codesetting2.Length > 0 ? $"101:{codesetting2}," : "";
                var data3 = codesetting3.Length > 0 ? $"102:{codesetting3}," : "";
                var data4 = codesetting4.Length > 0 ? $"103:{codesetting4}," : "";
                var data5 = codesetting5.Length > 0 ? $"104:{codesetting5}," : "";
                var data6 = codesetting6.Length > 0 ? $"105:{codesetting6}," : "";
                var data7 = codesetting7.Length > 0 ? $"106:{codesetting7}," : "";
                var data8 = codesetting8.Length > 0 ? $"107:{codesetting8}," : "";
                var data9 = codesetting9.Length > 0 ? $"108:{codesetting9}," : "";
                var data10 = codesetting10.Length > 0 ? $"109:{codesetting10}," : "";
                var data11 = codesetting11.Length > 0 ? $"110:{codesetting11}," : "";
                var data12 = codesetting12.Length > 0 ? $"111:{codesetting12}," : "";
                var translateddata = StringToHexConvertor.StringToHex(data1 + data2 + data3 +
                    data4 + data5 + data6 + data7 + data8 + data9 + data10 + data11 + data12);
                return translateddata;
            }
            else if(cmdcode == "05")
            {

                var data = "100,0,";
                var translateddata = StringToHexConvertor.StringToHex(data);
                return translateddata;
            }
            else
            {
                return "";
            }
        }
    }
}

using System;
using DataMatrix.net;
using System.Drawing;
using System.Collections.Generic;
using Direct.Shared;

namespace Direct.DMReaderUtility.Library
{
    [DirectSealed]
    [DirectDom("DM Utility Library")]
    [ParameterType(false)]
    public static class DMReaderUtil
    {
       
        [DirectDom("Get Information from the Data Matrix provided")]
        [DirectDomMethod("Get the information from {file location}")]
        [MethodDescriptionAttribute("Gets the information as a string from the DataMatrix image path provided")]
        public static string GetText(string dmFilePath)
        {
            DmtxImageDecoder decoder = new DmtxImageDecoder();
            List<string> dataExtract = decoder.DecodeImage((Bitmap)Bitmap.FromFile(dmFilePath), 1, new TimeSpan(0, 0, 3));
            String output = "";
            if (null != dataExtract && 0 < dataExtract.Count)
            {
                output= dataExtract[0];
            }
            else
            {
                output = "No Value extracted";
            }
            return output;
            }
        }
    
    
}

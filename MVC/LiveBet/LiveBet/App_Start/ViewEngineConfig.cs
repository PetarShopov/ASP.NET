﻿namespace LiveBet.App_Start
{
    using System.Web.Mvc;
    public class ViewEngineConfig
    {
        public static void RegisterViewEngines(ViewEngineCollection viewEngineCollection)
        {
            viewEngineCollection.Clear();
            RazorViewEngine razorEngine = new RazorViewEngine();

            razorEngine.PartialViewLocationFormats = new string[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Views/Shared/Partial/{0}.cshtml"
            };

            viewEngineCollection.Add(razorEngine);
        }
    }
}
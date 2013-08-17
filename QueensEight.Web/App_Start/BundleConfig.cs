﻿using System.Web;
using System.Web.Optimization;

namespace QueensEight.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(
                new ScriptBundle("~/bundles/vendor")
                    .Include("~/Scripts/jquery-{version}.js")
                    .Include("~/Scripts/bootstrap.js")
                    .Include("~/Scripts/underscore.js")
                    .Include("~/Scripts/angular.js")
                    .Include("~/Scripts/jquery.signalR-{version}.js")
                );

            bundles.Add(
                new ScriptBundle("~/bundles/queenseight")
                .Include("~/Scripts/app/app.js")
                .IncludeDirectory("~/Scripts/app/templates","*.js")
                .IncludeDirectory("~/Scripts/app/directives","*.js")
                .IncludeDirectory("~/Scripts/app/controllers","*.js")
                );

        }

    }
}
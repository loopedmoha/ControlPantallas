using Microsoft.Extensions.Logging;
using SuperfaldonWinUI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlPantallas.brainstorm
{
    class BSBuilder
    {
        private static string db = "PANTALLAS";

        //itemset("<SUPERFALDONES>Superfaldon/Oficial/Entra", "EVENT_RUN")
        private static string EventRunBuild(string evento, double delay = 0.0, double transition = 0.0)
        {
            var res = "";
            if (delay == 0.0)
            {
                if (string.IsNullOrWhiteSpace(evento))
                    evento = "";
                if (string.IsNullOrWhiteSpace(db))
                    db = "";

                // Construye el string
                res = $"itemset(\"<{db}>{evento}\", \"EVENT_RUN\");";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(evento))
                    evento = "";
                if (string.IsNullOrWhiteSpace(db))
                    db = "";

                // Construye el string
                res = $"itemgo(\"<{db}>{evento}\", \"EVENT_RUN\", {delay.ToString().Replace(",", ".")}, 0.0);";
            }
            return res;
        }
        private static string OrderString(string name, string type, string value)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(value))
                return "";

            var res = "";
            res = $"itemset(\"<{db}>{name}\", \"{type}\", \"{value}\");";


            return res;

        }


        public static string CarruselEntra(bool oficial)
        {
            var res = "";
            if (oficial)
            {
                res = EventRunBuild("Superfaldon/Oficial/Entra");
            }
            else
            {
                res = EventRunBuild("Superfaldon/Sondeo/Entra");
            }
            return res;
        }

        public static string LoadFileCurva(string file)
        {
            var res = "";
            if (file != "")
            {
               res = OrderString("FondoCurva", "TEX_FILE", file);
            }

            return res;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Blackjack;

namespace BlackjackViewPresenter
{
    public class BlackjackConfigPresenter
    {
        private readonly IBlackjackConfigView view;
        private IBlackjackConfig config;

        public BlackjackConfigPresenter(IBlackjackConfigView view, IBlackjackConfig config)
        {
            this.view = view;
            this.config = config;
        }

        public void PresentConfigToView()
        {
            Dictionary<string, string> strCfg = new();
            Type configType = config.GetType();

            foreach (PropertyInfo prop in configType.GetProperties())
            {
                string propName = prop.Name;
                string propVal = prop.GetValue(config)?.ToString() ?? "null";
                strCfg.Add(propName, propVal);
            }

            view.Config = strCfg;
        }

        public HashSet<string> SaveViewToConfig()
        {
            HashSet<string> badKeys = new();
            Type configType = config.GetType();
            IBlackjackConfig newConfig = (IBlackjackConfig)Activator.CreateInstance(configType);

            foreach ((string name, string value) in view.Config)
            {
                PropertyInfo prop = configType.GetProperty(name);
                if (prop != null)
                {
                    Type propType = prop.PropertyType;
                    object propValue = null;
                    bool ok = false;
                    switch (propType.Name)
                    {
                        case "Boolean":
                            ok = bool.TryParse(Capitalize(value), out bool bVal);
                            propValue = bVal;
                            break;
                        case "Int32":
                            ok = int.TryParse(value, out int iVal);
                            propValue = iVal;
                            break;
                        case "Double":
                            ok = double.TryParse(value, out double dVal);
                            propValue = dVal;
                            break;
                        default:
                            throw new ArgumentException($"unsupported type {propType.Name}");
                    }
                    if (ok)
                    {
                        prop.SetValue(newConfig, propValue);
                    }
                    else
                    {
                        badKeys.Add(name);
                    }
                }
                else
                {
                    badKeys.Add(name);
                }
            }


            if (badKeys.Count == 0)
            {
                config = newConfig;
            }

            return badKeys;
        }

        private void OnConfigChanged(object sender, EventArgs args)
        {
            SaveViewToConfig();
        }

        private static string Capitalize(string str)
        {
            if (str == null || str.Length == 0)
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }
    }
}

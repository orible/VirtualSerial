using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualSerial
{
    class ColourScheme
    {
        public class ColourSchemeData
        {
            public Color PanelBG, PanelFG, ButtonBG, ButtonFG, TextBoxBG, TextBoxFG;
            public ColourSchemeData(Color PanelBG, Color PanelFG)//, Color ButtonBG, Color ButtonFG, Color TextBoxBG)
            {
                this.PanelBG = PanelBG;
                this.PanelFG = PanelFG;
                this.ButtonBG = PanelBG;
                this.ButtonFG = PanelFG;
                this.TextBoxFG = PanelFG;
                this.TextBoxBG = PanelBG;
            }
        }

        public static IEnumerable<Control> GetSelfAndChildrenRecursive(Control parent)
        {
            List<Control> controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                controls.AddRange(GetSelfAndChildrenRecursive(child));
            }

            controls.Add(parent);

            return controls;
        }

        public static ColourSchemeData dark = new ColourSchemeData(Color.Black, Color.White);
        public static ColourSchemeData normal = new ColourSchemeData(Color.White, Color.Black);
        public static void ChangeTheme(ColourSchemeData scheme, Control container)
        {
            var list = GetSelfAndChildrenRecursive(container);
            foreach (Control component in list)
            {
                component.BackColor = scheme.PanelBG;
                component.ForeColor = scheme.PanelFG;

                if (component is Panel)
                {
                    //ChangeTheme(scheme, component.Controls);
                    component.BackColor = scheme.PanelBG;
                    component.ForeColor = scheme.PanelFG;
                }
                else if (component is Button)
                {
                    var btn = ((Button)component);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = scheme.ButtonFG;
                    component.BackColor = scheme.ButtonBG;
                    component.ForeColor = scheme.ButtonFG;
                }
                else if (component is TextBox)
                {
                    component.BackColor = scheme.TextBoxBG;
                    component.ForeColor = scheme.TextBoxFG;
                }
            }
        }
    }
}

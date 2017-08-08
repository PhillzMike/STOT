using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using System.IO;
namespace Front_End {
    /// <summary>
    /// Implement Crawler at intervals and other classes
    /// Author Seyi
    /// Google Looking Gui...DO IT
    /// </summary>
    /// <seealso cref="MaterialSkin.Controls.MaterialForm" />
    public partial class UNILAG: MaterialForm {
        public UNILAG() {
            InitializeComponent();
            var skinManager = MaterialSkinManager.Instance;
            skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            skinManager.ColorScheme = new ColorScheme(Primary.LightGreen100, Primary.LightGreen300, Primary.LightGreen300, Accent.LightBlue200, TextShade.WHITE);
        }

        private void UNILAG_Load(object sender, EventArgs e)
        {

        }

        

        
    }
}

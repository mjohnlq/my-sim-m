using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PersistenceLayer;
using BusinessEntity;

namespace StockInvestmentManagement.BaseData
{
    public partial class DeleZhuBi : Form
    {
        public DeleZhuBi()
        {
            InitializeComponent();
        }

        private void DeleZhuBi_Load(object sender, EventArgs e)
        {
            SqlDal db = new SqlDal();
            string maxDate = db.GetMaxZhuBiDate();
            string minDate = db.GetMinZhuBiDate();

            if (maxDate == null)
            {
                this.dtpEnd.Value = DateTime.Now;
            }
            else
            {
                this.dtpEnd.Value = new DateTime(Convert.ToInt32(maxDate.Substring(0, 4)), Convert.ToInt32(maxDate.Substring(4, 2)), Convert.ToInt32(maxDate.Substring(6, 2)));
            }

            if (minDate == null)
            {
                this.dtpStart.Value = DateTime.Now;
            }
            else
            {
                this.dtpStart.Value = new DateTime(Convert.ToInt32(minDate.Substring(0, 4)), Convert.ToInt32(minDate.Substring(4, 2)), Convert.ToInt32(minDate.Substring(6, 2)));
            }
        }

        private void cbDeleZhuBiByDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDeleZhuBiByDate.Checked)
            {
                cbTruncateZhuBi.Checked = false;
            }
            else
            {
                cbTruncateZhuBi.Checked = true;
            }
        }

        private void cbTruncateZhuBi_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTruncateZhuBi.Checked)
            {
                cbDeleZhuBiByDate.Checked = false;
            }
            else
            {
                cbDeleZhuBiByDate.Checked = true;
            }
        }

        private void btnDele_Click(object sender, EventArgs e)
        {
            SqlDal db = new SqlDal();
            if (cbTruncateZhuBi.Checked)
            {
                db.TruncateZhuBi();
            }
            else
            {
                string maxDate = dtpEnd.Value.Year.ToString() + ("0" + dtpEnd.Value.Month.ToString()).Substring(("0" + dtpEnd.Value.Month.ToString()).Length - 2, 2) + ("0" + dtpEnd.Value.Day.ToString()).Substring(("0" + dtpEnd.Value.Day.ToString()).Length - 2, 2);

                string minDate = dtpStart.Value.Year.ToString() + ("0" + dtpStart.Value.Month.ToString()).Substring(("0" + dtpStart.Value.Month.ToString()).Length - 2, 2) + ("0" + dtpStart.Value.Day.ToString()).Substring(("0" + dtpStart.Value.Day.ToString()).Length - 2, 2);

                db.DeleZhuBiByDate(maxDate, minDate);
            }

            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BaoHien.UI.Base
{
    public class KeyPressAwareDataGridView: DataGridView
    {
        protected override void OnControlAdded(ControlEventArgs e)
        {
            this.subscribeEvents(e.Control);
            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            this.unsubscribeEvents(e.Control);
            base.OnControlRemoved(e);
        }

        protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        {
            bool procesedInternally = false;

            if (this.keyPressHook != null)
            {
                this.keyPressHook(this, e);
                procesedInternally = e.SuppressKeyPress;
            }

            if (procesedInternally)
            {
                return true;
            }
            else
            {
                return base.ProcessDataGridViewKey(e);
            }
        }


        private void subscribeEvents(Control control)
        {
            control.KeyDown += new KeyEventHandler(this.control_KeyDown);
            control.ControlAdded += new ControlEventHandler(this.control_ControlAdded);
            control.ControlRemoved += new ControlEventHandler(this.control_ControlRemoved);

            foreach (Control innerControl in control.Controls)
            {
                this.subscribeEvents(innerControl);
            }
        }

        private void unsubscribeEvents(Control control)
        {
            control.KeyDown -= new KeyEventHandler(this.control_KeyDown);
            control.ControlAdded -= new ControlEventHandler(this.control_ControlAdded);
            control.ControlRemoved -= new ControlEventHandler(this.control_ControlRemoved);

            foreach (Control innerControl in control.Controls)
            {
                this.unsubscribeEvents(innerControl);
            }
        }

        private void control_ControlAdded(object sender, ControlEventArgs e)
        {
            this.subscribeEvents(e.Control);
        }

        private void control_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.unsubscribeEvents(e.Control);
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.keyPressHook != null)
            {
                this.keyPressHook(this, e);
            }
        }

        public event KeyEventHandler keyPressHook;

    }
}

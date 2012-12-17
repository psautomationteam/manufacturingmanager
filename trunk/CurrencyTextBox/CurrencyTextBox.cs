/*
 * Shows only currency information. 
 * Can return (string)$dollars.cents or (double)dollars.cents or (string)dollar.cents
 * 
 * Author: Philip Pierce
 * Date: 17 Feb 2006
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using System.Diagnostics;

namespace CurrencyTextBox
{
	public partial class CurrencyTextBox : TextBox
	{
		public CurrencyTextBox()
		{
		}

		#region Private Variables

		private string KeyPressRegex = @"^-?\$?\d*?(\.)?(\d{2})?$";
		private string OnValidatingRegex = @"^-?\$?\d*?(\.\d{2})?$";

		#endregion // Private Variables

		#region Properties

		private int m_Digits = -1;
		/// <summary>
		/// The number of digits to display (to the left of the decimal). -1 for unlimited
		/// </summary>
		[Description("The number of digits to display (to the left of the decimal). -1 for unlimited")]
		public int Digits
		{
			get { return m_Digits; }
			set
			{
				m_Digits = value;
				BuildRegStrings();
			}
		}

		private int m_Precision = 2;
		[Description("The number of decimal digits to display (to the right of the decimal)")]
		public int Precision
		{
			get { return m_Precision; }
			set
			{
				m_Precision = value;
				BuildRegStrings();
			}
		}

		/// <summary>
		/// returns the text with $ and ,
		/// </summary>
		public string Text_Formatting
		{
			get { return string.Format("{0:c}", System.Convert.ToDouble(this.Text.Replace("$", "").Replace(",", ""))); }
		}

		/// <summary>
		/// Returns the text without $ and ,
		/// </summary>
		public string Text_NoFormatting
		{
			get
			{ return this.Text.Replace("$", "").Replace(",", ""); }
		}

		/// <summary>
		/// Returns the text as a double. returns 0 if no text exists
		/// </summary>
		public double Text_AsDouble
		{
			get 
			{
				string tempStr = this.Text_NoFormatting.Trim();
				if (tempStr.Length > 0)
					return System.Convert.ToDouble(this.Text_NoFormatting);
				else
					return 0;
			}
		}

		#endregion // Properties

		#region Overrides

		/// <summary>
		/// set the mask when the control resizes
		/// </summary>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <summary>
		/// clear the formatting when the user enters the textbox
		/// </summary>
		protected override void OnEnter(EventArgs e)
		{
			BuildRegStrings();
			// remove formatting when the user enters the box
			this.Text = this.Text.Replace("$", "").Replace(",", "");
			base.OnEnter(e);
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
				case '\b': // backspace
				case '\r': // enter key
					break;
				default:
					// add current text plus the next key
					string tempStr = this.Text;
					// find out where the cursor is
					if (this.SelectionStart == this.Text.Length)
						tempStr = string.Format("{0}{1}", this.Text, e.KeyChar);
					else if (this.SelectionStart == 0)
						tempStr = string.Format("{0}{1}", e.KeyChar, this.Text);
					else // cursor is in the middle
						tempStr = string.Format("{0}{1}{2}",
							tempStr.Substring(0, this.SelectionStart),
							e.KeyChar,
							tempStr.Substring(this.SelectionStart, this.Text.Length - this.SelectionStart));


					// verify the text is valid
					if (!Regex.IsMatch(tempStr, KeyPressRegex))
					{
						Console.Beep();
						e.Handled = true;
					}
					break;
			}

			base.OnKeyPress(e);
		}

		/// <summary>
		/// make sure the text is in the correct format
		/// </summary>
		/// <param name="e"></param>
		protected override void OnValidating(CancelEventArgs e)
		{
			try
			{
				string tempStr = this.Text.Replace("$", "").Replace(",", "");
				// make sure the format is correct
				if (!Regex.IsMatch(tempStr, OnValidatingRegex))
				{
					Console.Beep();
					MessageBox.Show("Please enter data in a numeric format. Example: 123.45");
					e.Cancel = true;
				}
				else
					this.Text = string.Format("{0:c}", System.Convert.ToDouble(tempStr));
			}
			catch
			{
				Console.Beep();
				MessageBox.Show("Please enter data in a numeric format. Example: 123.45");
				e.Cancel = true;
			}

			finally
			{ base.OnValidating(e); }
		}

		/// <summary>
		/// Retrieve a double value from the text. returns -1 if invalid information is found
		/// </summary>
		/// <returns></returns> 
		public new object ValidateText()
		{
			string tempStr = this.Text_NoFormatting.Trim();
			if (tempStr.Length > 0)
				return System.Convert.ToDouble(this.Text_NoFormatting);
			else
				return 0;
		}

		#endregion // Overrides

		#region Functions

		/// <summary>
		/// sends error output to the debug window
		/// </summary>
		/// <param name="excep"></param>
		private void DebugMsg(System.Exception excep)
		{
			// show the error in debug window
			StringBuilder sb = new StringBuilder();
			sb.Append("Error in CurrencyTextbox\r\n");
			sb.Append("Error message: \r\n");
			sb.Append(excep.Message);
			Debug.WriteLine(sb.ToString());
		}

		/// <summary>
		/// Creates regex strings based on how many digits and the precision of the number
		/// </summary>
		public void BuildRegStrings()
		{
			// KeyPressRegex = @"^-?\$?\d*?(\.)?(\d{2})?$";
			// OnValidatingRegex = @"^-?\$?\d*?(\.\d{2})?$";

			StringBuilder sb = new StringBuilder();

			sb.Append(@"^-?\$?\d");

			// keypressregex, digits
			if (m_Digits < 0) // no limit on digits
				sb.Append(@"*"); 
			else
			{
				sb.Append(@"{,");
				sb.Append(m_Digits.ToString());
				sb.Append("}");
			}

			// keypressregex, precision
			sb.Append(@"?(\.)?(\d{0,");
			sb.Append(m_Precision.ToString());
			sb.Append(@"})?$");

			KeyPressRegex = @sb.ToString(); 


			// onvalidatingregex, digits
			sb = new StringBuilder();
			sb.Append(@"^-?\$?\d");

			if (m_Digits < 0) // no limit on digits
				sb.Append(@"*");
			else
			{
				sb.Append(@"{0,");
				sb.Append(m_Digits.ToString());
				sb.Append("}");
			}

			// onvalidatingregex, precision
			sb.Append(@"?(\.\d{0,");
			sb.Append(m_Precision.ToString());
			sb.Append(@"})?$");

			OnValidatingRegex = sb.ToString();
		}

		#endregion // Functions

	}
}

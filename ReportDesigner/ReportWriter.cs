/*
This library is free software; you can redistribute it and/or modify it under the terms of the GNU Lesser General
Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option)
any later version.

This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser General Public License for more
details.

You should have received a copy of the GNU Lesser General Public License along with this library; if not, write to
the Free Software Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 
*/

using System;
using System.IO;
using System.Collections;
using System.Data;
using daReport;


namespace daReportDesigner
{	
	public class ReportWriter
	{
		public ReportWriter()
		{

		}


		public static void WriteReport(string theFilename,ArrayList parameters,ICustomPaint[] staticObjects,ICustomPaint[] dynamicObjects,DaPrintDocument printDocument)
		{
			
			StreamWriter sw = new StreamWriter(theFilename,false,System.Text.Encoding.UTF8);
            //sw.Write("<?xml version='1.0' encoding='utf-8'?>");
			string theLayout = "Portrait";
			if ( printDocument.Layout == DaPrintDocument.LayoutType.Landscape )
				theLayout = "Landscape";            
			sw.WriteLine("<daReport papersize=\"" +printDocument.PaperType.ToString()+ "\" layout=\"" +theLayout+ "\">");
			sw.WriteLine("<margins left=\"" +printDocument.Margins.Left+ "\" top=\"" +printDocument.Margins.Top+ "\" right=\"" +printDocument.Margins.Right+ "\" bottom=\"" +printDocument.Margins.Bottom+ "\"></margins>");

			// write down report parameters
			if (parameters!=null || parameters.Count>0)
			{
				sw.WriteLine("<parameters>");
				for (int i=0;i<parameters.Count;i++)
				{
					sw.WriteLine("   <parameter name=\"" + parameters[i] + "\" />");
				}
				sw.WriteLine("</parameters>");
			}
			// end writing parameters

			sw.WriteLine("<content>");

			// writing down static objects
			if (staticObjects.Length>0)
			{
				sw.WriteLine("<staticContent>");
				for (int i=0;i<staticObjects.Length;i++)
				{
					if (staticObjects[i] is TextField)
					{
						TextField txtField = (TextField)staticObjects[i];
						sw.WriteLine(" ");
						sw.WriteLine("<textField name=\"" + txtField.Name.ToString() +"\" x=\"" + txtField.X + "\" y=\"" +txtField.Y+ "\" width=\""+txtField.Width+"\" height=\""+txtField.Height+"\" horAlignment=\""+txtField.HorizontalAlignment.ToString()+"\" verAlignment=\""+txtField.VerticalAlignment.ToString()+"\" Selectable=\"" + txtField.Selectable.ToString() + "\">");
						sw.WriteLine("<text horAlignment=\""+txtField.TextAlignment.ToString()+"\" verAlignment=\""+txtField.TextVerticalAlignment.ToString()+"\">"+ReplaceForXML(txtField.Text)+"</text>");

						if (txtField.Font != null)
						{
							string style = "";
							if (txtField.Font.Bold && txtField.Font.Italic)
								style = " style=\"Bold Italic\"";
							else if (txtField.Font.Bold)
								style = " style=\"Bold\"";
							else if (txtField.Font.Italic)
								style = " style=\"Italic\"";

							sw.WriteLine("<font family=\""+txtField.Font.Name+"\" size=\"" + txtField.Font.Size.ToString("f00") +"\" "+style+"></font>");
						}

						sw.WriteLine("<foregroundColor color=\""+txtField.ForegroundColor.Name+"\"></foregroundColor>");
						sw.WriteLine("<backgroundColor color=\""+txtField.BackgroundColor.Name+"\"></backgroundColor>");
						sw.WriteLine("<border width=\""+txtField.BorderWidth.ToString()+"\" color=\""+txtField.BorderColor.Name+"\"></border>");
						sw.WriteLine("</textField>");
					}
					else if (staticObjects[i] is PictureBox)
					{
						PictureBox pictureBox = (PictureBox)staticObjects[i];
						sw.WriteLine(" ");
                        sw.WriteLine("<pictureBox name=\"" + pictureBox.Name.ToString() + "\" x=\"" + pictureBox.X + "\" y=\"" + pictureBox.Y + "\" width=\"" + pictureBox.Width + "\" height=\"" + pictureBox.Height + "\" stretch=\"" + (pictureBox.Stretch == true ? "true" : "false") + "\"  horAlignment=\"" + pictureBox.HorizontalAlignment.ToString() + "\" verAlignment=\"" + pictureBox.VerticalAlignment.ToString() + "\" Selectable=\"" + pictureBox.Selectable.ToString() + "\">");

						sw.WriteLine(@"<file>" + (pictureBox.ImageFile==null?" ":ReplaceForXML(pictureBox.ImageFile)) + "</file>");

						sw.WriteLine("<border width=\""+pictureBox.BorderWidth.ToString()+"\" color=\""+pictureBox.BorderColor.Name+"\"></border>");
						sw.WriteLine("</pictureBox>");
					}
					else if (staticObjects[i] is ChartBox)
					{
						ChartBox chartBox = (ChartBox)staticObjects[i];
						
						string style = "";
						if (chartBox.Type == ChartBox.ChartType.Pie)
							style = " type=\"Pie\"";
						else
							style = " type=\"Bars\"";


						sw.WriteLine(" ");
						sw.WriteLine("<chartBox name=\"" + ReplaceForXML(chartBox.Name) + "\" x=\"" + chartBox.X + "\" y=\"" +chartBox.Y+ "\" width=\""+chartBox.Width+"\" height=\""+chartBox.Height+"\" "+style+" horAlignment=\""+chartBox.HorizontalAlignment.ToString()+"\" verAlignment=\""+chartBox.VerticalAlignment.ToString()+"\" Selectable=\"" + chartBox.Selectable.ToString() + "\">");

						sw.WriteLine("<title>" + ReplaceForXML(chartBox.Title) + "</title>");

						if (chartBox.TitleFont != null)
						{
							string fontStyle = "";
							if (chartBox.TitleFont.Bold && chartBox.TitleFont.Italic)
								fontStyle = " style=\"Bold Italic\"";
							else if (chartBox.TitleFont.Bold)
								fontStyle = " style=\"Bold\"";
							else if (chartBox.TitleFont.Italic)
								fontStyle = " style=\"Italic\"";

							sw.WriteLine("<titleFont family=\""+chartBox.TitleFont.Name+"\" size=\"" + chartBox.TitleFont.Size.ToString("f00") +"\" "+fontStyle+"></titleFont>");
						}

						sw.WriteLine("<xLabel>" + ReplaceForXML(chartBox.XLabel) + "</xLabel>");

						if (chartBox.LabelFont != null)
						{
							string fontStyle = "";
							if (chartBox.LabelFont.Bold && chartBox.LabelFont.Italic)
								fontStyle = " style=\"Bold Italic\"";
							else if (chartBox.LabelFont.Bold)
								fontStyle = " style=\"Bold\"";
							else if (chartBox.LabelFont.Italic)
								fontStyle = " style=\"Italic\"";

							sw.WriteLine("<labelFont family=\""+chartBox.LabelFont.Name+"\" size=\"" + chartBox.LabelFont.Size.ToString("f00") +"\" "+fontStyle+"></labelFont>");
						}
						
						sw.WriteLine("<mapAreaColor>"+chartBox.MapAreaColor.Name+"</mapAreaColor>");
						sw.WriteLine("<showLegend>"+chartBox.ShowLegend.ToString()+"</showLegend>");
						sw.WriteLine("<border width=\""+chartBox.BorderWidth.ToString()+"\" color=\""+chartBox.BorderColor.Name+"\"></border>");
						sw.WriteLine("</chartBox>");
					}
					else if (staticObjects[i] is StyledTable)
					{
						// wrtie down table data
						StyledTable styledTable = (StyledTable)staticObjects[i];
						sw.WriteLine(" ");
						sw.WriteLine("<table x=\"" + styledTable.X + "\" y=\"" +styledTable.Y+ "\" width=\""+styledTable.Width+"\" height=\""+styledTable.Height+"\" drawEmptyRows=\"" +(styledTable.DrawEmptyRows==true?"true":"false")+ "\" cellHeight=\"" + styledTable.CellHeight.ToString()+ "\" horAlignment=\""+styledTable.HorizontalAlignment.ToString()+"\" verAlignment=\""+styledTable.VerticalAlignment.ToString()+"\" Selectable=\""+styledTable.Selectable.ToString()+"\">");

						string style = "";
						if (styledTable.DataFont.Bold && styledTable.DataFont.Italic)
							style = " style=\"Bold Italic\"";
						else if (styledTable.DataFont.Bold)
							style = " style=\"Bold\"";
						else if (styledTable.DataFont.Italic)
							style = " style=\"Italic\"";
						sw.WriteLine("<font family=\""+styledTable.DataFont.Name+"\" size=\"" + styledTable.DataFont.Size.ToString("f00") +"\" "+style+"></font>");

						sw.WriteLine("<header headerColor=\""+styledTable.HeaderBackgroundColor.Name+"\" headerFontColor=\"" + styledTable.HeaderFontColor.Name + "\">");

						style = "";
						if (styledTable.HeaderFont.Bold && styledTable.HeaderFont.Italic)
							style = " style=\"Bold Italic\"";
						else if (styledTable.HeaderFont.Bold)
							style = " style=\"Bold\"";
						else if (styledTable.HeaderFont.Italic)
							style = " style=\"Italic\"";

						sw.WriteLine("<font family=\""+styledTable.HeaderFont.Name+"\" size=\"" + styledTable.HeaderFont.Size.ToString("f00") +"\" "+style+"></font>");

						sw.WriteLine("</header>");

						int numOfColumns = styledTable.Columns.Length;

						sw.WriteLine("<columns>");
						for (int j=0;j<numOfColumns;j++)
						{
							string textAlignment = "Left";
							if (styledTable.Columns[j].Alignment==StyledTableColumn.AlignmentType.Center)
								textAlignment = "Center";
							else if (styledTable.Columns[j].Alignment==StyledTableColumn.AlignmentType.Right)
								textAlignment = "Right";
							else
								textAlignment = "Left";

							sw.WriteLine("<column name=\""+ReplaceForXML(styledTable.Columns[j].Name)+"\"  label=\""+ReplaceForXML(styledTable.Columns[j].Label)+"\"  width=\"" +styledTable.Columns[j].Width + "\" align=\"" + textAlignment + "\"  />");
						}

						sw.WriteLine("</columns>");

						if (styledTable.Data != null)
						{
							sw.WriteLine("<data>");

							for (int j=0;j<styledTable.Data.Rows.Count;j++)
							{
								sw.WriteLine("   <record>");
								
								DataRow theRow = styledTable.Data.Rows[j];
								for (int k=0;k<styledTable.Data.Columns.Count;k++)
								{
									sw.WriteLine("      <field>" + ReplaceForXML(theRow[k].ToString()) + "</field>");
								}

								sw.WriteLine("   </record>");
							}
							sw.WriteLine("</data>");
						}

						sw.WriteLine("</table>");
					}

				}
				sw.WriteLine("</staticContent>");
			}

			// end writing static objects

			sw.WriteLine(" ");
			if (dynamicObjects.Length>0)
			{
				sw.WriteLine("<dynamicContent>");
				for (int i=0;i<dynamicObjects.Length;i++)
				{
					if (dynamicObjects[i] is TextField)
					{
						TextField txtField = (TextField)dynamicObjects[i];
						sw.WriteLine(" ");
                        sw.WriteLine("<textField name=\"" + txtField.Name.ToString() + "\" x=\"" + txtField.X + "\" y=\"" + txtField.Y + "\" width=\"" + txtField.Width + "\" height=\"" + txtField.Height + "\" horAlignment=\"" + txtField.HorizontalAlignment.ToString() + "\" verAlignment=\"" + txtField.VerticalAlignment.ToString() + "\" Selectable=\"" + txtField.Selectable.ToString() + "\">");
						sw.WriteLine("<text horAlignment=\""+txtField.TextAlignment.ToString()+"\" verAlignment=\""+txtField.TextVerticalAlignment.ToString()+"\">"+ReplaceForXML(txtField.Text)+"</text>");

						if (txtField.Font != null)
						{
							string style = "";
							if (txtField.Font.Bold && txtField.Font.Italic)
								style = " style=\"Bold Italic\"";
							else if (txtField.Font.Bold)
								style = " style=\"Bold\"";
							else if (txtField.Font.Italic)
								style = " style=\"Italic\"";

							sw.WriteLine("<font family=\""+txtField.Font.Name+"\" size=\"" + txtField.Font.Size.ToString("f00") +"\" "+style+"></font>");
						}

						sw.WriteLine("<foregroundColor color=\""+txtField.ForegroundColor.Name+"\"></foregroundColor>");
						sw.WriteLine("<backgroundColor color=\""+txtField.BackgroundColor.Name+"\"></backgroundColor>");
						sw.WriteLine("<border width=\""+txtField.BorderWidth.ToString()+"\" color=\""+txtField.BorderColor.Name+"\"></border>");
						sw.WriteLine("</textField>");
					}					
					else if (dynamicObjects[i] is StyledTable)
					{
						StyledTable styledTable = (StyledTable)dynamicObjects[i];
						sw.WriteLine(" ");
						sw.WriteLine("<table x=\"" + styledTable.X + "\" y=\"" +styledTable.Y+ "\" width=\""+styledTable.Width+"\" height=\""+styledTable.Height+"\" borderColor=\"" + styledTable.BorderColor.Name + "\" drawEmptyRows=\"" +(styledTable.DrawEmptyRows==true?"true":"false")+ "\" cellHeight=\"" + styledTable.CellHeight.ToString()+ "\" dataSource=\"" + ReplaceForXML(styledTable.DataSource)+ "\"  horAlignment=\""+styledTable.HorizontalAlignment.ToString()+"\" verAlignment=\""+styledTable.VerticalAlignment.ToString()+"\" Selectable=\"" + styledTable.Selectable.ToString() + "\" GroupByField=\"" + styledTable.GroupByField + "\">");

						sw.WriteLine("<header headerColor=\""+styledTable.HeaderBackgroundColor.Name+"\" headerFontColor=\"" + styledTable.HeaderFontColor.Name + "\">");

						string style = "";
						if (styledTable.HeaderFont.Bold && styledTable.HeaderFont.Italic)
							style = " style=\"Bold Italic\"";
						else if (styledTable.HeaderFont.Bold)
							style = " style=\"Bold\"";
						else if (styledTable.HeaderFont.Italic)
							style = " style=\"Italic\"";

						sw.WriteLine("<font family=\""+styledTable.HeaderFont.Name+"\" size=\"" + styledTable.HeaderFont.Size.ToString("f00") +"\" "+style+"></font>");

						sw.WriteLine("</header>");

						sw.WriteLine("<dataRows dataFontColor=\"" + styledTable.DataFontColor.Name + "\">");

						style = "";
						if (styledTable.DataFont.Bold && styledTable.DataFont.Italic)
							style = " style=\"Bold Italic\"";
						else if (styledTable.DataFont.Bold)
							style = " style=\"Bold\"";
						else if (styledTable.DataFont.Italic)
							style = " style=\"Italic\"";
						sw.WriteLine("<font family=\""+styledTable.DataFont.Name+"\" size=\"" + styledTable.DataFont.Size.ToString("f00") +"\" "+style+"></font>");

						sw.WriteLine("</dataRows>");

						int numOfColumns = styledTable.Columns.Length;

						sw.WriteLine("<columns>");
						for (int j=0;j<numOfColumns;j++)
						{
							string textAlignment = "Left";
							if (styledTable.Columns[j].Alignment==StyledTableColumn.AlignmentType.Center)
								textAlignment = "Center";
							else if (styledTable.Columns[j].Alignment==StyledTableColumn.AlignmentType.Right)
								textAlignment = "Right";
							else
								textAlignment = "Left";

							sw.WriteLine("<column name=\""+ReplaceForXML(styledTable.Columns[j].Name)+"\"  label=\""+ReplaceForXML(styledTable.Columns[j].Label)+"\" FormatMask=\"" +ReplaceForXML(styledTable.Columns[j].FormatMask)+ "\" width=\"" +styledTable.Columns[j].Width + "\" align=\"" + textAlignment + "\" Visible=\"" + styledTable.Columns[j].Visible + "\"/>");
						}
						sw.WriteLine("</columns>");
						sw.WriteLine("</table>");
					}

				}
				sw.WriteLine("</dynamicContent>");
			}
			sw.WriteLine(" ");
			sw.WriteLine("</content>");
			sw.WriteLine("</daReport>");
			sw.Close();            
		}


		private static string ReplaceForXML(string sText)
		{
			sText = sText.Replace("&","&amp;"); //&amp; -> &
			sText = sText.Replace("'","&apos;"); //&apos; -> '
			sText = sText.Replace("\"","&quot;"); //&quot; -> "
			sText = sText.Replace("<","&lt;"); //&lt; -> <
			sText = sText.Replace(">","&gt;"); //&gt; -> >
			return sText;
		}
	}
}

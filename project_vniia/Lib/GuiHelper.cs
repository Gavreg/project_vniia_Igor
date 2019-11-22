using System.Windows.Forms;
using System;

namespace project_vniia.Lib
{
    class GuiHelper
    {
        static public void filter(DataGridView gridView, TextBox filtr)
        {
            #region bad
            //for (int i = 0; i < gridView.Rows.Count -0; i++)
            //{
            //    gridView.Rows[i].Visible = gridView[0, i].Value.ToString() == filtr.Text;
            //}

            //for (int i = 0; i < gridView.Rows.Count - 1; i++)
            //{
            //    gridView.CurrentCell = null;
            //    gridView.Rows[i].Visible = false;
            //    for (int c = 0; c < gridView.Columns.Count; c++)
            //    {
            //        if (gridView[c, i].Value.ToString() == filtr.Text)
            //        {
            //            gridView.Rows[i].Visible = true;
            //            break;
            //        }
            //    }
            //}
            #endregion
            var i_max = gridView.RowCount;
            for (int i = 0; i < i_max; ++i)
            {
                gridView.CurrentCell = null;
                gridView.Rows[i].Selected = false;
                for (int j = 0; j < gridView.ColumnCount; ++j)
                    if (gridView.Rows[i].Cells[j].Value != null)
                    {
                        gridView.Rows[i].Visible = false;
                        if (gridView.Rows[i].Cells[j].Value.ToString().Contains(filtr.Text))
                        {
                            gridView.Rows[i].Selected = true;
                            gridView.Rows[i].Visible = true;
                            break;
                        }
                    }
            }
        }
        }
    }


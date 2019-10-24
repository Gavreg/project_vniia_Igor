namespace project_vniia
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.номерБДDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.типБДDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.номерФЭУDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.номинальноеUDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.примечанияDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.местоположениеDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.отметкаВыполненияDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.кАНDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sColLineageDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.sGenerationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sGUIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sLineageDataGridViewImageColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.блокиBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.tCPM82_NewDataSet = new project_vniia.TCPM82_NewDataSet();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.cANNoteTableAdapter = new project_vniia.TCPM82_NewDataSetTableAdapters.CANNoteTableAdapter();
            this.блокиTableAdapter = new project_vniia.TCPM82_NewDataSetTableAdapters.БлокиTableAdapter();
            this.tCPM82NewDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tCPM82NewDataSetBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button_filtr = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.блокиBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCPM82_NewDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCPM82NewDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCPM82NewDataSetBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.номерБДDataGridViewTextBoxColumn,
            this.типБДDataGridViewTextBoxColumn,
            this.номерФЭУDataGridViewTextBoxColumn,
            this.номинальноеUDataGridViewTextBoxColumn,
            this.примечанияDataGridViewTextBoxColumn,
            this.местоположениеDataGridViewTextBoxColumn,
            this.отметкаВыполненияDataGridViewTextBoxColumn,
            this.кАНDataGridViewTextBoxColumn,
            this.sColLineageDataGridViewImageColumn,
            this.sGenerationDataGridViewTextBoxColumn,
            this.sGUIDDataGridViewTextBoxColumn,
            this.sLineageDataGridViewImageColumn});
            this.dataGridView1.DataSource = this.блокиBindingSource2;
            this.dataGridView1.Location = new System.Drawing.Point(1, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(623, 644);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // номерБДDataGridViewTextBoxColumn
            // 
            this.номерБДDataGridViewTextBoxColumn.DataPropertyName = "Номер БД";
            this.номерБДDataGridViewTextBoxColumn.HeaderText = "Номер БД";
            this.номерБДDataGridViewTextBoxColumn.Name = "номерБДDataGridViewTextBoxColumn";
            // 
            // типБДDataGridViewTextBoxColumn
            // 
            this.типБДDataGridViewTextBoxColumn.DataPropertyName = "Тип БД";
            this.типБДDataGridViewTextBoxColumn.HeaderText = "Тип БД";
            this.типБДDataGridViewTextBoxColumn.Name = "типБДDataGridViewTextBoxColumn";
            // 
            // номерФЭУDataGridViewTextBoxColumn
            // 
            this.номерФЭУDataGridViewTextBoxColumn.DataPropertyName = "Номер ФЭУ";
            this.номерФЭУDataGridViewTextBoxColumn.HeaderText = "Номер ФЭУ";
            this.номерФЭУDataGridViewTextBoxColumn.Name = "номерФЭУDataGridViewTextBoxColumn";
            // 
            // номинальноеUDataGridViewTextBoxColumn
            // 
            this.номинальноеUDataGridViewTextBoxColumn.DataPropertyName = "Номинальное U";
            this.номинальноеUDataGridViewTextBoxColumn.HeaderText = "Номинальное U";
            this.номинальноеUDataGridViewTextBoxColumn.Name = "номинальноеUDataGridViewTextBoxColumn";
            // 
            // примечанияDataGridViewTextBoxColumn
            // 
            this.примечанияDataGridViewTextBoxColumn.DataPropertyName = "Примечания";
            this.примечанияDataGridViewTextBoxColumn.HeaderText = "Примечания";
            this.примечанияDataGridViewTextBoxColumn.Name = "примечанияDataGridViewTextBoxColumn";
            // 
            // местоположениеDataGridViewTextBoxColumn
            // 
            this.местоположениеDataGridViewTextBoxColumn.DataPropertyName = "Местоположение";
            this.местоположениеDataGridViewTextBoxColumn.HeaderText = "Местоположение";
            this.местоположениеDataGridViewTextBoxColumn.Name = "местоположениеDataGridViewTextBoxColumn";
            // 
            // отметкаВыполненияDataGridViewTextBoxColumn
            // 
            this.отметкаВыполненияDataGridViewTextBoxColumn.DataPropertyName = "Отметка выполнения";
            this.отметкаВыполненияDataGridViewTextBoxColumn.HeaderText = "Отметка выполнения";
            this.отметкаВыполненияDataGridViewTextBoxColumn.Name = "отметкаВыполненияDataGridViewTextBoxColumn";
            // 
            // кАНDataGridViewTextBoxColumn
            // 
            this.кАНDataGridViewTextBoxColumn.DataPropertyName = "КАН";
            this.кАНDataGridViewTextBoxColumn.HeaderText = "КАН";
            this.кАНDataGridViewTextBoxColumn.Name = "кАНDataGridViewTextBoxColumn";
            // 
            // sColLineageDataGridViewImageColumn
            // 
            this.sColLineageDataGridViewImageColumn.DataPropertyName = "s_ColLineage";
            this.sColLineageDataGridViewImageColumn.HeaderText = "s_ColLineage";
            this.sColLineageDataGridViewImageColumn.Name = "sColLineageDataGridViewImageColumn";
            // 
            // sGenerationDataGridViewTextBoxColumn
            // 
            this.sGenerationDataGridViewTextBoxColumn.DataPropertyName = "s_Generation";
            this.sGenerationDataGridViewTextBoxColumn.HeaderText = "s_Generation";
            this.sGenerationDataGridViewTextBoxColumn.Name = "sGenerationDataGridViewTextBoxColumn";
            // 
            // sGUIDDataGridViewTextBoxColumn
            // 
            this.sGUIDDataGridViewTextBoxColumn.DataPropertyName = "s_GUID";
            this.sGUIDDataGridViewTextBoxColumn.HeaderText = "s_GUID";
            this.sGUIDDataGridViewTextBoxColumn.Name = "sGUIDDataGridViewTextBoxColumn";
            // 
            // sLineageDataGridViewImageColumn
            // 
            this.sLineageDataGridViewImageColumn.DataPropertyName = "s_Lineage";
            this.sLineageDataGridViewImageColumn.HeaderText = "s_Lineage";
            this.sLineageDataGridViewImageColumn.Name = "sLineageDataGridViewImageColumn";
            // 
            // блокиBindingSource2
            // 
            this.блокиBindingSource2.DataMember = "Блоки";
            this.блокиBindingSource2.DataSource = this.tCPM82_NewDataSet;
            // 
            // tCPM82_NewDataSet
            // 
            this.tCPM82_NewDataSet.DataSetName = "TCPM82_NewDataSet";
            this.tCPM82_NewDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(656, 12);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(240, 644);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // comboBox1
            // 
            this.comboBox1.DisplayMember = "Номер БД";
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(732, 662);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.ValueMember = "Номер БД";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cANNoteTableAdapter
            // 
            this.cANNoteTableAdapter.ClearBeforeFill = true;
            // 
            // блокиTableAdapter
            // 
            this.блокиTableAdapter.ClearBeforeFill = true;
            // 
            // tCPM82NewDataSetBindingSource
            // 
            this.tCPM82NewDataSetBindingSource.DataSource = this.tCPM82_NewDataSet;
            this.tCPM82NewDataSetBindingSource.Position = 0;
            // 
            // tCPM82NewDataSetBindingSource2
            // 
            this.tCPM82NewDataSetBindingSource2.DataSource = this.tCPM82_NewDataSet;
            this.tCPM82NewDataSetBindingSource2.Position = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button_filtr
            // 
            this.button_filtr.Location = new System.Drawing.Point(12, 662);
            this.button_filtr.Name = "button_filtr";
            this.button_filtr.Size = new System.Drawing.Size(75, 23);
            this.button_filtr.TabIndex = 3;
            this.button_filtr.Text = "Фильтрация";
            this.button_filtr.UseVisualStyleBackColor = true;
            this.button_filtr.Click += new System.EventHandler(this.button_filtr_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 695);
            this.Controls.Add(this.button_filtr);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.блокиBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCPM82_NewDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCPM82NewDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCPM82NewDataSetBindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private TCPM82_NewDataSet tCPM82_NewDataSet;
        private TCPM82_NewDataSetTableAdapters.CANNoteTableAdapter cANNoteTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView2;
        private TCPM82_NewDataSetTableAdapters.БлокиTableAdapter блокиTableAdapter;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource tCPM82NewDataSetBindingSource;
        private System.Windows.Forms.BindingSource tCPM82NewDataSetBindingSource2;
        private System.Windows.Forms.BindingSource блокиBindingSource2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn номерБДDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn типБДDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn номерФЭУDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn номинальноеUDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn примечанияDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn местоположениеDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn отметкаВыполненияDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn кАНDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn sColLineageDataGridViewImageColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sGenerationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sGUIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewImageColumn sLineageDataGridViewImageColumn;
        private System.Windows.Forms.Button button_filtr;
    }
}


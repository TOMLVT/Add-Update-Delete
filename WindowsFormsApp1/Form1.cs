using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        // Khai báo chuỗi kết nối


        string chuoiKN = "";
     
        SqlConnection KetNoi;

        DataSet duLieu;

        SqlDataAdapter boDocGhi;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

          

           
        }


        // Kích hoạt kết nối cho FORM 
        private void Form1_Load(object sender, EventArgs e)
        {
            chuoiKN = @"Data Source=LAPTOP-DOCKI20T;Initial Catalog=QuanLyBanSach;Persist Security Info=True;User ID=sa;Password=197004;TrustServerCertificate=True";

            KetNoi = new SqlConnection(chuoiKN);

            string chuoiLEnh = "select * from Sach";

            boDocGhi = new SqlDataAdapter(chuoiLEnh, chuoiKN);

            duLieu = new DataSet();

            boDocGhi.Fill(duLieu);

            dataGridView_ChuDe.DataSource = duLieu.Tables[0];

        }


        // nút thêm dữ liệu 
        private void btn_Them_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder bophatsinh = new SqlCommandBuilder(boDocGhi);
            // nút thêm 
            DataRow row = duLieu.Tables[0].NewRow();
            row["MaTruyen"] = txt_MaSach.Text;
            row["TenSach"] = txt_TenSach.Text;
            row["Gia"] = txt_Gia.Text;

            duLieu.Tables[0].Rows.Add(row);
            MessageBox.Show("Thêm sách thành công!" ,"Thông báo " , MessageBoxButtons.OK, MessageBoxIcon.Information);
            clearData();
            boDocGhi.Update(duLieu);
        }


        // NÚt cập nhật 
        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
           try
            {
                if (dataGridView_ChuDe.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn dòng để cập nhật !", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int RowIndexUpdate = dataGridView_ChuDe.CurrentRow.Index;

                if (RowIndexUpdate >= 0 && RowIndexUpdate < duLieu.Tables[0].Rows.Count)
                {
                    DataRow UpdateRow = duLieu.Tables[0].Rows[RowIndexUpdate];
                    UpdateRow["MaTruyen"] = txt_MaSach.Text;
                    UpdateRow["TenSach"] = txt_TenSach.Text;
                    UpdateRow["Gia"] = txt_Gia.Text;

                    SqlCommandBuilder bophatsinh = new SqlCommandBuilder(boDocGhi);
                    boDocGhi.Update(duLieu);
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }           

            } catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật dữ liệu !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        // NÚt xóa 
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
           if (dataGridView_ChuDe.CurrentRow != null)
            {
                int rowIndex = dataGridView_ChuDe.CurrentRow.Index;

                if (rowIndex >= 0 && rowIndex < duLieu.Tables[0].Rows.Count)
                {
                 
                    duLieu.Tables[0].Rows[rowIndex].Delete();

                  
                    SqlCommandBuilder bophatsinh = new SqlCommandBuilder(boDocGhi);
                    boDocGhi.Update(duLieu);

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dữ liệu để xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridView_ChuDe_SelectionChanged(object sender, EventArgs e)
        {
        }


        // gọi lại hàm dọn dữ liệu 
        private void btn_clear_Click(object sender, EventArgs e)
        {
          clearData();
        }


        // Hàm dọn dữ liệu -> cho nút dọn dữ liệu
        public void clearData()
        {
            txt_MaSach.Text = "";
            txt_TenSach.Text = "";
            txt_Gia.Text = "";
        }


        // Hàm chọn kích hoạt sự kiện hiển thị dữ liệu trong TextBox cho việc UPDATE dữ liệu 
        private void dataGridView_ChuDe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView_ChuDe.Rows.Count)
            {
               
                DataGridViewRow selectedRow = dataGridView_ChuDe.Rows[e.RowIndex];

             
                txt_MaSach.Text = selectedRow.Cells["MaTruyen"].Value?.ToString() ?? "";
                txt_TenSach.Text = selectedRow.Cells["TenSach"].Value?.ToString() ?? "";
                txt_Gia.Text = selectedRow.Cells["Gia"].Value?.ToString() ?? "";
            }
        }
    }
}

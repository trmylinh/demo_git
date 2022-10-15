using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TH4
{
    public partial class frmMatHang : Form
    {
        Classess.DataBaseProcess dtbase = new Classess.DataBaseProcess();


        public frmMatHang()
        {
            InitializeComponent();

        }
        private void HienChiTiet(bool hien)
        {
            txtMaSP.Enabled = hien;
            txtTenSP.Enabled = hien;
            dtpNgayHH.Enabled = hien;
            dtpNgaySX.Enabled = hien;
            txtDonvi.Enabled = hien;
            txtDongia.Enabled = hien;
            txtGhichu.Enabled = hien;
            //Ẩn hiện 2 nút Lưu và Hủy
            btnLuu.Enabled = hien;
            btnHuy.Enabled = hien;
        }


        private void frmMatHang_Load(object sender, EventArgs e)
        {
            // load du lieu len DataGridView
            dgvMatHang.DataSource = dtbase.DataReader("Select * from tblMatHang");
            // an nut Sua, Xoa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            // cap nhat tren nhan tieu de
            lblTieuDe.Text = "TIM KIEM MAT HANG";

            // cam nut Sua va Xoa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // viet cau lenh sql cho tim kiem
            string sql = "select * from tblMatHang where MaSP is not null";
            // tim theo MaSP khac rong
            if (txtMaSP.Text.Trim() != "")
            {
                sql += " and MaSP like '%" + txtTKMaSP + "%'";
            }
            // kiem tra TenSP
            if (txtTKTenSP.Text.Trim() != "")
            {
                sql += " and TenSP like N'%" + txtTKTenSP.Text + "%'";
            }

            // load du lieu tim duoc len dataGridView
            dgvMatHang.DataSource = dtbase.DataReader(sql);
        }

        private void dgvMatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // hien thi nut Sua
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnThem.Enabled = false;
            // bat loi khi nguoi dung kich chuoi linh tinh len dataGridView
            try
            {
                txtMaSP.Text = dgvMatHang.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dgvMatHang.CurrentRow.Cells[1].Value.ToString();
                dtpNgaySX.Value = (DateTime)dgvMatHang.CurrentRow.Cells[2].Value;
                dtpNgayHH.Value = (DateTime)dgvMatHang.CurrentRow.Cells[3].Value;
                txtDonvi.Text = dgvMatHang.CurrentRow.Cells[4].Value.ToString();
                txtDongia.Text = dgvMatHang.CurrentRow.Cells[5].Value.ToString();
                txtGhichu.Text = dgvMatHang.CurrentRow.Cells[6].Value.ToString();
            }
            catch (Exception ex)
            {

            }

        }

        private void DeleteAllChiTiet()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            dtpNgaySX.Value = DateTime.Today;
            dtpNgayHH.Value = DateTime.Today;
            txtDonvi.Text = "";
            txtDongia.Text = "";
            txtGhichu.Text = "";

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            lblTieuDe.Text = "THEM MAT HANG";
            DeleteAllChiTiet();
            // cam nut sua, xoa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            // hien groupbox chi tiet
            HienChiTiet(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            // cap nhat tieu de
            lblTieuDe.Text = "CAP NHAT MAT HANG";
            // cam nut sua, xoa
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            // hien groupbox chi tiet
            HienChiTiet(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Bật Message Box cảnh báo người sử dụng
            if (MessageBox.Show("Bạn có chắc chắn xóa mã mặt hàng " +
           txtMaSP.Text + " không ? Nếu có ấn nút Lưu, không thì ấn nút Hủy",
           "Xóa sản phẩm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                lblTieuDe.Text = "XÓA MẶT HÀNG";
                btnThem.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql = "";
            //Chúng ta sử dụng control ErrorProvider để hiển thị lỗi
            //Kiểm tra tên sản phầm có bị để trống không
            if (txtTenSP.Text.Trim() == "")
            {
                errChiTiet.SetError(txtTenSP, "Bạn không để trống tên sản phẩm!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            //Kiểm tra ngày sản xuất, lỗi nếu người sử dụng nhập vào ngày sản xuất lớn hơn ngày hiện tại
            if (dtpNgaySX.Value > DateTime.Now)
            {
                errChiTiet.SetError(dtpNgaySX, "Ngày sản xuất không hợp lệ!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            //Kiểm tra ngày hết hạn xem có lớn hơn ngày sản xuất không
            if (dtpNgayHH.Value < dtpNgaySX.Value)
            {
                errChiTiet.SetError(dtpNgayHH, "Ngay hết hạn nhỏ hơn ngày sản xuất!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            //Kiểm tra đơn vị xem có để trống không
            if (txtDonvi.Text.Trim() == "")
            {
                errChiTiet.SetError(txtDonvi, "Bạn không để trống đơn vi!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            //Kiểm tra đơn giá
            if (txtDongia.Text.Trim() == "")
            {
                errChiTiet.SetError(txtDongia, "Bạn không để trống đơn giá!");
                return;
            }
            else
            {
                errChiTiet.Clear();
            }
            //Nếu nút Thêm enable thì thực hiện thêm mới
            //Dùng ký tự N' trước mỗi giá trị kiểu text để insert giá trị có dấu tiếng việt vào CSDL được đúng
            if (btnThem.Enabled == true)
            { //Kiểm tra xem ô nhập MaSP có bị trống không if
                if (txtMaSP.Text.Trim() == "")
                {
                    errChiTiet.SetError(txtMaSP, "Bạn không để trống mã sản phẩm trường này!");
                return;
                }
                else
                { //Kiểm tra xem mã sản phẩm đã tồn tại chưa đẻ tránh việc insert mới bị lỗi
                     sql = "Select * From tblMatHang Where MaSP ='" + txtMaSP.Text +
                    "'";
                    DataTable dtSP = dtbase.DataReader(sql);
                    if (dtSP.Rows.Count > 0)
                    {
                        errChiTiet.SetError(txtMaSP, "Mã sản phẩm trùng trong cơ sở dữ liệu");
                     return;
                    }
                    errChiTiet.Clear();
                }
                //Insert vao CSDL
                sql = "INSERT INTO tblMatHang(MaSP, TenSP, NgaySX, NgayHH, DonVi, DonGia, GhiChu) VALUES(";
                sql += "N'" + txtMaSP.Text + "',N'" + txtTenSP.Text + "','" + dtpNgaySX.Value.Date + "','" + dtpNgayHH.Value.Date + "',N'" + txtDonvi.Text + "',N'" +txtDongia.Text + "',N'" + txtGhichu.Text + "')";
            }
            //Nếu nút Sửa enable thì thực hiện cập nhật dữ liệu
            if (btnSua.Enabled == true)
            {
                sql = "Update tblMatHang SET ";
                sql += "TenSP = N'" + txtTenSP.Text + "',";
                sql += "NgaySX = '" + dtpNgaySX.Value.Date + "',";
                sql += "NgayHH = '" + dtpNgayHH.Value.Date + "',";
                sql += "DonVi = N'" + txtDonvi.Text + "',";
                sql += "DonGia = '" + txtDongia.Text + "',";
                sql += "GhiChu = N'" + txtGhichu.Text + "' ";
                sql += "Where MaSP = N'" + txtMaSP.Text + "'";
            }
            //Nếu nút Xóa enable thì thực hiện xóa dữ liệu
            if (btnXoa.Enabled == true)
            {
                sql = "Delete From tblMatHang Where MaSP =N'" + txtMaSP.Text + "'";
            }
            dtbase.DataChange(sql);
            //Cap nhat lai DataGrid
            sql = "Select * from tblMatHang";
            dgvMatHang.DataSource = dtbase.DataReader(sql);
            //Ẩn hiện các nút phù hợp chức năng
            HienChiTiet(false);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;


        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            //Thiết lập lại các nút như ban đầu
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            //xoa trang chi tiết
            DeleteAllChiTiet();
            //Cam nhap vào groupBox chi tiết
            HienChiTiet(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "TB", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();

        }
    }
}

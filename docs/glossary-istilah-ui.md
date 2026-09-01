# Glosarium Istilah UI Baku — PT PERSADA
### Sistem Manajemen Ruangan & Pemesanan

Dokumen ini menjadi acuan baku penerjemahan dan pelabelan antarmuka pengguna (UI) agar konsisten 100% berbahasa Indonesia di seluruh modul aplikasi.

---

## 1. Status Entri & Transaksi

| Bahasa Inggris (Lama) | Bahasa Indonesia Baku (Baru) | Token Warna (CSS) | Deskripsi / Konteks |
| :--- | :--- | :--- | :--- |
| `Approved` / `DISETUJUI` | **Disetujui** | `--color-success` (`--color-success-soft`) | Permohonan pemesanan/fasilitas telah disetujui admin. |
| `Pending` / `MENUNGGU` | **Menunggu** | `--color-warning` (`--color-warning-soft`) | Menunggu verifikasi atau tindakan peninjauan lanjutan. |
| `Rejected` / `DITOLAK` | **Ditolak** | `--color-danger` (`--color-danger-soft`) | Permohonan ditolak oleh admin atau alasan kepatuhan. |
| `Processing` / `PROCESSING` | **Diproses** | `--color-info` (`--color-info-soft`) | Permintaan fasilitas atau tiket sedang dalam penanganan. |
| `New` / `BARU` | **Baru** | `--color-accent` (`--color-accent-soft`) | Entri atau permintaan baru masuk ke antrean. |
| `Completed` / `COMPLETED` | **Selesai** | `--color-success` (`--color-success-soft`) | Tiket IT, fasilitas, atau laporan telah selesai tuntas. |
| `Open` / `TERBUKA` | **Terbuka** | `--color-danger` (`--color-danger-soft`) | Laporan bug atau isu baru belum ditangani. |
| `In Progress` / `DIKERJAKAN` | **Dikerjakan** | `--color-warning` (`--color-warning-soft`) | Tiket atau bug sedang aktif dikerjakan oleh tim IT. |
| `Confirmed` / `DIKONFIRMASI` | **Dikonfirmasi** | `--color-success` (`--color-success-soft`) | Reservasi atau jadwal terkonfirmasi valid. |
| `Active` / `AKTIF` | **Aktif** | `--color-success` (`--color-success-soft`) | Akun, ruangan, atau fasilitas dalam status aktif. |
| `Inactive` / `TIDAK AKTIF` | **Tidak Aktif** | `--color-danger` (`--color-danger-soft`) | Akun atau entri dinonaktifkan. |
| `Cancelled` / `DIBATALKAN` | **Dibatalkan** | `--color-danger` (`--color-danger-soft`) | Sesi atau pesanan dibatalkan oleh pemohon. |
| `Maintenance` / `PERAWATAN` | **Dalam Perawatan** / **Atur Pemeliharaan** | `--color-warning` (`--color-warning-soft`) | Ruangan atau fasilitas sedang pemeliharaan rutin. |
| `Active Now` | **Aktif Sekarang** | `--color-success` (`--color-success-soft`) | Status sesi login perangkat yang sedang digunakan. |

---

## 2. Aksi & Tombol Antarmuka

| Istilah Bahasa Inggris | Istilah Baku Indonesia | Contoh Pemakaian |
| :--- | :--- | :--- |
| `Submit` | **Kirim** | Tombol submit formulir, kirim tiket |
| `Cancel` | **Batal** | Tombol pembatalan dialog/modal |
| `Save` / `Save Changes` | **Simpan** / **Simpan Perubahan** | Formulir profil dan konfigurasi |
| `Save Preferences` | **Simpan Preferensi** | Simpan preferensi notifikasi |
| `Discard Changes` | **Batalkan Perubahan** | Batal ubah pengaturan |
| `Edit` | **Ubah** / **Edit** | Tombol ubah data |
| `Delete` | **Hapus** | Tombol hapus entri/akun |
| `Search` | **Cari** | Placeholder kotak pencarian ("Cari menu, pemesanan, atau pengguna...") |
| `Export` / `Export Report` | **Ekspor** / **Ekspor Laporan** | Tombol unduh laporan CSV/Excel |
| `Filter` / `Reset Filter` | **Filter** / **Reset Filter** (atau **Atur Ulang Filter**) | Kontrol pemfilteran tabel |
| `Loading` / `Loading...` | **Memuat...** | State tunggu data |
| `READ-ONLY` | **HANYA BACA** | Badge informasi log audit sistem |
| `ATUR MAINTENANCE` | **ATUR PEMELIHARAAN** | Aksi log audit / pemeliharaan ruangan |
| `Revoke` | **Cabut Akses** | Tombol cabut sesi perangkat di Pengaturan |
| `Log Out of All Other Sessions` | **Keluar dari Semua Sesi Lain** | Tombol putus seluruh login aktif lain |
| `Verify` | **Verifikasi** | Tombol konfirmasi kode 2FA |
| `Resend Code` | **Kirim Ulang** | Minta ulang kode OTP / 2FA |
| `View All` | **Lihat Semua** | Tautan navigasi tabel |
| `Back` | **Kembali** | Tombol kembali ke halaman sebelumnya |
| `Download` | **Unduh** | Tombol download file/bukti |
| `Upload` | **Unggah** | Tombol upload avatar/lampiran |

---

## 3. Modul & Navigasi

| Modul Asal | Nama Bahasa Indonesia Baku |
| :--- | :--- |
| `System Reports` | **Laporan Sistem** |
| `Room Booking` | **Pemesanan Ruangan** |
| `Facility Requests` | **Permintaan Fasilitas** |
| `Account Deletion` | **Penghapusan Akun** |
| `IT Support` | **Dukungan IT** |
| `Bug Reports` | **Laporan Bug** |
| `All Reports` | **Semua Aktivitas** |
| `Audit Log` | **Audit Log** / **Log Audit Sistem** |
| `User Management` | **Manajemen Pengguna** |
| `Dashboard` | **Dasbor** |
| `Profile` | **Profil Pengguna** |
| `Settings` | **Pengaturan** |
| `Change Password` | **Ubah Kata Sandi** |
| `2-Step Verification` | **PIN / Verifikasi 2 Langkah** |
| `Active Sessions & Devices` | **Sesi & Perangkat Aktif** |
| `Notifications` | **Notifikasi** |
| `Language & Timezone` | **Bahasa & Zona Waktu** |
| `Operational Hours & Booking Rules` | **Jam Operasional & Aturan Pemesanan** |
| `Role Management & Permissions` | **Manajemen Peran & Hak Akses** |
| `Calendar & Email Integration` | **Integrasi Kalender & Email** |
| `Brand Logo & Colors` | **Logo & Warna Brand** |
| `Support` | **Bantuan** |
| `Logout` | **Keluar** |

---

## 4. Format & Tipografi Standar
- **Bobot Font Maksimum:** `600` (Semi-bold).
- **Semua Judul/Teks:** Menggunakan Sentence Case atau Title Case Indonesia yang wajar. Dilarang menggunakan UPPERCASE keras berlebihan pada tabel dan judul.
- **Warna Status:** Wajib memakai token CSS semantik (`--color-success`, `--color-warning`, `--color-danger`, `--color-info`, `--color-accent`) dan padanan soft-nya.

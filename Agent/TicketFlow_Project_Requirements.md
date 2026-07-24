# Tên dự án: TicketFlow

**Mô tả:** Một nền tảng phân phối vé sự kiện và quản lý chỗ ngồi thời gian thực (Real-time Event Ticketing & Seating System). Nền tảng này cho phép các Ban tổ chức (Organizer) tự tạo sự kiện, thiết lập sơ đồ chỗ ngồi, và bán vé. Khách hàng (Customer) có thể vào xem, chọn ghế cụ thể và thanh toán.

**Điểm mấu chốt (Pain point):** Hệ thống phải chịu tải cao trong những đợt mở bán vé sự kiện "hot" (ví dụ: concert của BlackPink hay Taylor Swift) và tuyệt đối không được xảy ra tình trạng overbooking (hai người mua cùng một ghế).

## 1. Phân quyền người dùng (Roles)
Hệ thống có 3 role chính:

* **Admin:** Quản trị viên hệ thống (Duyệt sự kiện, quản lý user, xem doanh thu tổng, cấu hình phí nền tảng).
* **Organizer (Ban tổ chức):** Người tạo và quản lý sự kiện, sơ đồ ghế, xem thống kê doanh thu/vé bán ra của sự kiện mình quản lý, thực hiện scan vé check-in.
* **Customer (Khách hàng):** Người tìm kiếm sự kiện, chọn ghế, đặt vé, thanh toán và lưu trữ vé (QR Code).

## 2. Yêu cầu chức năng (Functional Requirements)

### 2.1. Module Quản lý User & Authentication
* Đăng ký/Đăng nhập (Email/Password và Social Login: Google/Facebook).
* Sử dụng JWT (JSON Web Token) để xác thực. Có cơ chế Refresh Token để bảo mật.
* Flow quên mật khẩu (gửi OTP qua email).

### 2.2. Module Quản lý Sự kiện & Địa điểm (Event & Venue)
* **Venue (Địa điểm):** Organizer có thể tạo địa điểm, định nghĩa các khu vực (Zone A, Zone B, VIP, Standard), số hàng, số ghế trong mỗi khu vực.
* **Event (Sự kiện):** Tạo sự kiện, gán vào một Venue cụ thể. Cấu hình giá tiền cho từng Zone hoặc từng ghế.
* Sự kiện có các trạng thái: Draft, Pending Approval (Chờ Admin duyệt), Published, Sold Out, Completed, Cancelled.

### 2.3. Module Đặt vé (Booking Engine - Quan trọng nhất)
* Khách hàng có thể xem sơ đồ ghế của sự kiện (thông qua API trả về trạng thái của từng ghế).
* **Seat Locking Mechanism (Cơ chế giữ ghế):** Khi khách hàng chọn ghế và nhấn "Thanh toán", ghế đó phải được khóa tạm thời (Hold) trong 10 phút.
    * Trong 10 phút này, người khác không thể chọn ghế đó.
    * Nếu sau 10 phút chưa thanh toán thành công, ghế sẽ tự động được "nhả" ra cho người khác mua.
* **Giới hạn:** Một user chỉ được mua tối đa 4 vé cho một sự kiện "Hot".

### 2.4. Module Thanh toán & Webhook (Payment)
* Tích hợp cổng thanh toán (Bạn có thể giả lập Stripe hoặc VNPay).
* Hệ thống phải cung cấp Webhook endpoint để nhận callback từ cổng thanh toán báo về trạng thái giao dịch (Success, Failed).
* Xử lý logic khi nhận callback: Nếu thanh toán thành công, chuyển trạng thái vé sang Paid và gửi email. Nếu thất bại, giải phóng ghế.
* **Lưu ý:** Cần xử lý Idempotency cho Webhook để tránh trường hợp cổng thanh toán gọi callback 2 lần gây nhân đôi vé hoặc lỗi dữ liệu.

### 2.5. Module Quản lý Vé (Ticket & Check-in)
* Vé thành công sẽ được sinh ra một mã QR Code (chứa thông tin mã hóa) và gửi qua Email/App cho khách hàng.
* Cung cấp API cho ứng dụng của Organizer để scan QR code check-in tại cổng. API phải trả về nhanh chóng (vé hợp lệ, vé giả, hoặc vé đã check-in rồi).

## 3. Yêu cầu phi chức năng (Non-Functional Requirements - Tiêu chuẩn thực tế)
* **Concurrency Control (Xử lý đồng thời):** Đây là bài toán khó nhất. Bạn phải thiết kế Database và logic code (ví dụ dùng Optimistic/Pessimistic Locking trong RDBMS hoặc Distributed Lock bằng Redis) để đảm bảo 1.000 người cùng click vào 1 ghế ở cùng 1 giây (Race Condition), chỉ có 1 người duy nhất giữ được ghế.
* **Caching:** Các dữ liệu ít thay đổi nhưng đọc nhiều (Danh sách sự kiện, thông tin sự kiện) cần được cache lại (ví dụ dùng Redis) để giảm tải cho Database.
* **Rate Limiting:** Chống bot spam API đặt vé bằng cách giới hạn số lượng request từ 1 IP hoặc 1 User (ví dụ: tối đa 5 requests/giây).
* **Database:** Ưu tiên dùng hệ quản trị CSDL quan hệ (PostgreSQL hoặc MySQL) vì tính chất Transactional (ACID) của nghiệp vụ tài chính và đặt chỗ.
* **Logging:** Ghi log lại mọi giao dịch thay đổi trạng thái của vé và thanh toán (Audit Log) để phục vụ tra soát sau này.

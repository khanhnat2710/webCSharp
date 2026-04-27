using BlazorWeb.Models;

namespace BlazorWeb.Services.PaymentMethods;

public interface IPaymentService
{
    //Lấy toàn bộ
    Task<List<PaymentMethod>> GetAllPaymentMethodsAsync();
    //Lấy theo id
    Task<PaymentMethod> GetPaymentMethodAsync(int id);
    //Thêm
    Task CreatePaymentMethodAsync(PaymentMethod paymentMethod);
    //Sửa
    Task UpdatePaymentMethodAsync(PaymentMethod paymentMethod);
    //Xóa
    Task DeletePaymentMethodAsync(int id);
}
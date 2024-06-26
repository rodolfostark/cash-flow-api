using CashFlow.Domain.Enums;

namespace CashFlow.Domain.Extentions;
public static class PaymentTypeExtentions
{
    public static string PaymentTypeToString(this PaymentType paymentType)
    {
        return paymentType switch
        {
            PaymentType.Cash => "Dinheiro",
            PaymentType.CreditCard => "Cartão de Crédito",
            PaymentType.DebitCard => "Cartão de Débito",
            PaymentType.EletronicTransfer => "Transferência Bancária",
            _ => string.Empty
        };
    }
}

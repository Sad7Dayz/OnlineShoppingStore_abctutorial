//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShoppingStore.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_ShippingDetails
    {
        public int ShippingDetailId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string PaymentType { get; set; }
    
        public virtual Tbl_Members Tbl_Members { get; set; }
        public virtual Tbl_Members Tbl_Members1 { get; set; }
        public virtual Tbl_Members Tbl_Members2 { get; set; }
        public virtual Tbl_Members Tbl_Members3 { get; set; }
        public virtual Tbl_Members Tbl_Members4 { get; set; }
        public virtual Tbl_Members Tbl_Members5 { get; set; }
    }
}
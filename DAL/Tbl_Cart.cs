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
    
    public partial class Tbl_Cart
    {
        public int CartId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> CartStatusId { get; set; }
    
        public virtual Tbl_Product Tbl_Product { get; set; }
        public virtual Tbl_Product Tbl_Product1 { get; set; }
        public virtual Tbl_Product Tbl_Product2 { get; set; }
        public virtual Tbl_Product Tbl_Product3 { get; set; }
    }
}

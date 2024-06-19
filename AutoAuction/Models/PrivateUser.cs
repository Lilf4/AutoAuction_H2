namespace AutoAuction.Models {
    public class PrivateUser : User {
        public string CPR { get; set; }

        public PrivateUser(int id, string username, string postcode, decimal balance, string CPR) : base(id, username, postcode, balance) {
            this.CPR = CPR;
        }

        public override string ToString() {
            return base.ToString() + $", CPR: {CPR}";
        }
    }
}

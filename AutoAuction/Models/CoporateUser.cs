namespace AutoAuction.Models {
    public class CoporateUser : User {

        public decimal CreditLimit { get; set; }
        public string CVR { get; set; }
        public CoporateUser(int id, string username, int postcode, decimal balance, decimal creditLimit, string cvr) : base(id, username, postcode, balance) {
            this.CreditLimit = creditLimit;
            this.CVR = cvr;
        }

        public override string ToString() {
            return base.ToString() + $", Credit Limit: {CreditLimit}, CVR: {CVR}";
        }
    }
}

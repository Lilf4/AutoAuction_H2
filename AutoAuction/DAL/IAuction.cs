namespace AutoAuction.DAL {
    public interface IAuction {
		void CreateAuction();
		void PlaceBid();
        void GetAuction();
        void SearchAuction();
    }
}

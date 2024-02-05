using Microsoft.EntityFrameworkCore;
using ChocolateFactory.Models; // Adjust this using directive based on the actual namespace of your model classes.

namespace ChocolateFactory.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<BillOfMaterial> BillOfMaterials { get; set; }
        public DbSet<BusinessEntity> BusinessEntities { get; set; }
        public DbSet<BusinessEntityAddress> BusinessEntityAddresses { get; set; }
        public DbSet<BusinessEntityContact> BusinessEntityContacts { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<CountryRegionCurrency> CountryRegionCurrencies { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
        public DbSet<EmployeePayHistory> EmployeePayHistories { get; set; }
        public DbSet<Illustration> Illustrations { get; set; }
        public DbSet<JobCandidate> JobCandidates { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PersonCreditCard> PersonCreditCards { get; set; }
        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<PhoneNumberType> PhoneNumberTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCostHistory> ProductCostHistories { get; set; }
        public DbSet<ProductDescription> ProductDescriptions { get; set; }
        public DbSet<ProductInventory> ProductInventories { get; set; }
        public DbSet<ProductListPriceHistory> ProductListPriceHistories { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }
        public DbSet<ProductModelIllustration> ProductModelIllustrations { get; set; }
        public DbSet<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCultures { get; set; }
        public DbSet<ProductPhoto> ProductPhotos { get; set; }
        public DbSet<ProductProductPhoto> ProductProductPhotos { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
        public DbSet<ProductVendor> ProductVendors { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
        public DbSet<SalesOrderHeaderSalesReason> SalesOrderHeaderSalesReasons { get; set; }
        public DbSet<SalesPerson> SalesPeople { get; set; }
        public DbSet<SalesPersonQuotaHistory> SalesPersonQuotaHistories { get; set; }
        public DbSet<SalesReason> SalesReasons { get; set; }
        public DbSet<SalesTaxRate> SalesTaxRates { get; set; }
        public DbSet<SalesTerritory> SalesTerritories { get; set; }
        public DbSet<SalesTerritoryHistory> SalesTerritoryHistories { get; set; }
        public DbSet<ScrapReason> ScrapReasons { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShipMethod> ShipMethods { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<SpecialOfferProduct> SpecialOfferProducts { get; set; }
        public DbSet<StateProvince> StateProvinces { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<TransactionHistoryArchive> TransactionHistoryArchives { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }

        public DbSet<VAdditionalContactInfo> VAdditionalContactInfo { get; set; }
        public DbSet<VEmployee> VEmployee { get; set; }
        public DbSet<VEmployeeDepartment> VEmployeeDepartment { get; set; }
     
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<VIndividualCustomer> VIndividualCustomers { get; set; }
        public DbSet<VJobCandidate> VJobCandidates { get; set; }
        public DbSet<VJobCandidateEducation> VJobCandidateEducations { get; set; }
        public DbSet<VJobCandidateEmployment> VJobCandidateEmployments { get; set; }
        public DbSet<VPersonDemographic> VPersonDemographics { get; set; }
        public DbSet<VProductAndDescription> VProductAndDescriptions { get; set; }
        public DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; }
        public DbSet<VProductModelInstruction> VProductModelInstructions { get; set; }
        public DbSet<VSalesPerson> VSalesPeople { get; set; }
        public DbSet<VSalesPersonSalesByFiscalYear> VSalesPersonSalesByFiscalYears { get; set; }
        public DbSet<VStateProvinceCountryRegion> VStateProvinceCountryRegions { get; set; }
        public DbSet<VStoreWithAddress> VStoreWithAddresses { get; set; }
        public DbSet<VStoreWithContact> VStoreWithContacts { get; set; }
        public DbSet<VStoreWithDemographic> VStoreWithDemographics { get; set; }
        public DbSet<VVendorWithAddress> VVendorWithAddresses { get; set; }
        public DbSet<VVendorWithContact> VVendorWithContacts { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderRouting> WorkOrderRoutings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("your_connection_string_here");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Product and BillOfMaterial for Component
            modelBuilder.Entity<BillOfMaterial>()
                .HasOne(b => b.Component) // Navigation property in BillOfMaterial
                .WithMany(p => p.BillOfMaterialComponents) // Assuming you have a collection property in Product for components
                .HasForeignKey(b => b.ComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Configure the delete behavior if required

            // Configure the one-to-many relationship between Product and BillOfMaterial for ProductAssembly
            modelBuilder.Entity<BillOfMaterial>()
                .HasOne(b => b.ProductAssembly) // Navigation property in BillOfMaterial
                .WithMany(p => p.BillOfMaterialProductAssemblies) // Assuming you have a collection property in Product for assemblies
                .HasForeignKey(b => b.ProductAssemblyId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Configure the delete behavior if required

            // Configure the one-to-one relationship between BillOfMaterial and UnitMeasure
            modelBuilder.Entity<BillOfMaterial>()
                .HasOne(b => b.UnitMeasureCodeNavigation)
                .WithMany() // If UnitMeasure doesn't have a navigation property back to BillOfMaterial, use WithMany without parameters
                .HasForeignKey(b => b.UnitMeasureCode);

            modelBuilder.Entity<SalesOrderHeader>()
       .HasOne(s => s.BillToAddress)
       .WithMany(a => a.SalesOrderHeaderBillToAddresses)
       .HasForeignKey(s => s.BillToAddressId)
       .OnDelete(DeleteBehavior.Restrict) // or another behavior that suits your use case
       .IsRequired();

            // Relationship configuration for Address -> SalesOrderHeader (ShipToAddress)
            modelBuilder.Entity<SalesOrderHeader>()
                .HasOne(s => s.ShipToAddress)
                .WithMany(a => a.SalesOrderHeaderShipToAddresses)
                .HasForeignKey(s => s.ShipToAddressId)
                .OnDelete(DeleteBehavior.Restrict) // or another behavior that suits your use case
                .IsRequired();

            base.OnModelCreating(modelBuilder); // Don't forget to call the base method
        }

    }
}

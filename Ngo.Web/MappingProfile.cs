namespace Ngo.Web
{
    using AutoMapper;
    using Data.Lib;
    using Data.Lib.Entity;
    using Ngo.Web.Models;

    namespace Samity
    {
        public class MappingProfile : Profile
        {
            public MappingProfile()
            {

                CreateMap<Member, MemberModel>().ReverseMap();
                CreateMap<Book, BookModel>().ReverseMap();
                CreateMap<Deposit, DepositCollectionModel>().ReverseMap();

                //CreateMap<LoanCollection, LoanCollectionViewModel>().ReverseMap();
                //// CreateMap<Loan, LoanViewModel> ().ForMember (src => src.LoanCollectionViewModel, opt => opt.MapFrom (src => src.LoanCollections));
                //CreateMap<Nominee, NomineeViewModel>().ReverseMap();
                //// CreateMap<DepositCollection, DepositCollectionViewModel> ().ReverseMap ();
                //CreateMap<Book, BookViewModel>().ForMember(m => m.PaymentMethodViewModel, opt => opt.MapFrom(src => src.PaymentMethod)).ReverseMap();


                //CreateMap<Account, AccountViewModel>()
                //    .ForMember(src => src.DepositCollectionViewModel, opt => opt.MapFrom(src => src.DepositCollection))
                //    .ForMember(src => src.LoanViewModel, opt => opt.MapFrom(src => src.Loan))
                //    .ForMember(src => src.MemberViewModel, opt => opt.MapFrom(src => src.Member))
                //    .ForMember(src => src.NomineeViewModel, opt => opt.MapFrom(src => src.Nominee))
                //    .ForMember(src => src.BookViewModel, opt => opt.MapFrom(src => src.Book));

                //CreateMap<AccountViewModel, Account>()
                //        .ForMember(dest => dest.DepositCollection, opt => opt.Ignore())
                //        .ForMember(src => src.Loan, opt => opt.Ignore())
                //        .ForMember(src => src.Member, opt => opt.Ignore())
                //        .ForMember(src => src.Nominee, opt => opt.Ignore())
                //        .ForMember(src => src.Book, opt => opt.Ignore());
                ////   CreateMap<PaymentMethod, PaymentMethodViewModel>().ReverseMap();
                //CreateMap<DepositCollectionViewModel, DepositCollection>();
                //CreateMap<DepositCollection, DepositCollectionViewModel>();
            }

        }
    }
}

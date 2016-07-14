using EstimatedBudget.ViewModels;
using EstimatedBudget.ViewModels.BankAccount;
using EstimatedBudget.ViewModels.Category;
using EstimatedBudget.ViewModels.Levy;
using EstimatedBudget.ViewModels.Mode;
using EstimatedBudget.ViewModels.Registration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;


namespace EstimatedBudget.Helpers
{
    public class ViewModelLocator
    {
        static IUnityContainer BankAccountContainer;
        static IUnityContainer CategoryContainer;
        static IUnityContainer LevyContainer;
        static IUnityContainer ModeContainer;
        static IUnityContainer RegistrationContainer;

        static IUnityContainer MainContainer;
        static ViewModelLocator()
        {
            BankAccountContainer = new UnityContainer();
            BankAccountContainer.AddNewExtension<Interception>();
            BankAccountContainer.RegisterType<BankAccountViewModel>().Configure<Interception>().SetInterceptorFor<BankAccountViewModel>(new VirtualMethodInterceptor());

            CategoryContainer = new UnityContainer();
            CategoryContainer.AddNewExtension<Interception>();
            CategoryContainer.RegisterType<CategoryViewModel>().Configure<Interception>().SetInterceptorFor<CategoryViewModel>(new VirtualMethodInterceptor());

            LevyContainer = new UnityContainer();
            LevyContainer.AddNewExtension<Interception>();
            LevyContainer.RegisterType<LevyViewModel>().Configure<Interception>().SetInterceptorFor<LevyViewModel>(new VirtualMethodInterceptor());

            ModeContainer = new UnityContainer();
            ModeContainer.AddNewExtension<Interception>();
            ModeContainer.RegisterType<ModeViewModel>().Configure<Interception>().SetInterceptorFor<ModeViewModel>(new VirtualMethodInterceptor());

            RegistrationContainer = new UnityContainer();
            RegistrationContainer.AddNewExtension<Interception>();
            RegistrationContainer.RegisterType<RegistrationViewModel>().Configure<Interception>().SetInterceptorFor<RegistrationViewModel>(new VirtualMethodInterceptor());

            MainContainer = new UnityContainer();
            MainContainer.AddNewExtension<Interception>();
            MainContainer.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager()).Configure<Interception>().SetInterceptorFor<MainViewModel>(new VirtualMethodInterceptor());
        }

        public BankAccountViewModel BankAccount
        {
            get { return BankAccountContainer.Resolve<BankAccountViewModel>(); }
        }
       
        public CategoryViewModel Category
        {
            get { return CategoryContainer.Resolve<CategoryViewModel>(); }
        }
        public LevyViewModel Levy
        {
            get { return LevyContainer.Resolve<LevyViewModel>(); }
        }

        public ModeViewModel Mode
        {
            get { return ModeContainer.Resolve<ModeViewModel>(); }
        }

        public RegistrationViewModel Registration
        {
            get { return RegistrationContainer.Resolve<RegistrationViewModel>(); }
        }

        public MainViewModel Main
        {
            get { return MainContainer.Resolve<MainViewModel>(); }
        }
    }
}

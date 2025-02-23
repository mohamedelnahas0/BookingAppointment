using AutoMapper;
using BookingAppointment.Api.Extensions.RequestModelExtensions;
using BookingAppointment.Application.DTOS.Appointment;
using BookingAppointment.Application.DTOS.Category;
using BookingAppointment.Application.DTOS.Customer;
using BookingAppointment.Application.DTOS.Customers;
using BookingAppointment.Application.DTOS.DeliveryInfos;
using BookingAppointment.Application.DTOS.Feedback;
using BookingAppointment.Application.DTOS.Notification;
using BookingAppointment.Application.DTOS.OrderItems;
using BookingAppointment.Application.DTOS.Orderss;
using BookingAppointment.Application.DTOS.Payments;
using BookingAppointment.Application.DTOS.Product;
using BookingAppointment.Application.DTOS.Schedule;
using BookingAppointment.Application.DTOS.Service;
using BookingAppointment.Application.DTOS.ServiceProvider;
using BookingAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingAppointment.Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Product mappings
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.ImageUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            // Category mappings
            CreateMap<Category, CategoryToReturnDto>();

            // Feedback mappings
            CreateMap<CreateFeedbackDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();
            CreateMap<Feedback, FeedbackDetailDto>();

            // Customer mappings
            CreateMap<Customer, CustomerDto>();

            // Service mappings
            CreateMap<Service, ServiceDto>()
                .ForMember(dest => dest.ServiceProviders, opt => opt.MapFrom(src => src.ServiceProviders));
            CreateMap<Service, ServiceDetailDto>();
            CreateMap<CreateServiceDto, Service>();
            CreateMap<UpdateServiceDto, Service>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // ServiceProvider mappings
            CreateMap<ServiceProvider, ServiceProviderDto>();
            CreateMap<ServiceProvider, ServiceProviderDetailDto>();
            CreateMap<CreateServiceProviderDto, ServiceProvider>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => new Schedule
                {
                    DayOfWeek = src.Schedule.DayOfWeek,
                    StartTime = TimeSpan.Parse(src.Schedule.StartTime),
                    EndTime = TimeSpan.Parse(src.Schedule.EndTime)
                }));
            CreateMap<UpdateServiceProviderDto, ServiceProvider>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Schedule mappings
        




            CreateMap<Schedule, ScheduleDtoProvider>()
           .ForMember(dest => dest.ServiceProviderName,
                     opt => opt.MapFrom(src => $"{src.ServiceProvider.FirstName} {src.ServiceProvider.LastName}"));
            CreateMap<ScheduleCreateDto, Schedule>()
     .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
     .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));
            CreateMap<ScheduleCreateDto, ScheduleDto>().ReverseMap();
            CreateMap<ScheduleDto, Schedule>().ReverseMap();

            CreateMap<UpdateScheduleDto, Schedule>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            CreateMap<ScheduleCreateDto, Schedule>()
          .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => (int)src.DayOfWeek))
          .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
          .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime));

            // Entity to DTO mapping
            CreateMap<Schedule, ScheduleDto>()
                .ForMember(dest => dest.StartTime,
                    opt => opt.MapFrom(src => src.StartTime.ToString(@"hh\:mm\:ss")))
                .ForMember(dest => dest.EndTime,
                    opt => opt.MapFrom(src => src.EndTime.ToString(@"hh\:mm\:ss")));

            // Additional mappings for provider info if needed
            CreateMap<Schedule, ScheduleDtoProvider>()
                .ForMember(dest => dest.ServiceProviderName,
                    opt => opt.MapFrom(src => $"{src.ServiceProvider.FirstName} {src.ServiceProvider.LastName}"));

            //order
         
        
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CustomerName,
                    opt => opt.MapFrom(src =>
                        $"{src.Customer.FirstName} {src.Customer.LastName}"))
                .ForMember(dest => dest.OrderItems,
                    opt => opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.DeliveryInfo,
                    opt => opt.MapFrom(src => src.DeliveryInfo))
                .ForMember(dest => dest.Payments,
                    opt => opt.MapFrom(src => src.Payments));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(dest => dest.OrderDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => OrderStatus.Pending));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.TotalPrice,
                    opt => opt.MapFrom(src => src.Quantity * src.UnitPrice));

            CreateMap<OrderItemCreateDto, OrderItem>();
            //orderitems
            CreateMap<OrderItem, OrderItemDetailDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));


            //payment
            CreateMap<Payment, PaymentDto>();
            CreateMap<ProcessPaymentRequest, Payment>()
                .ForMember(dest => dest.PaymentDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => PaymentStatus.Pending));

            // Delivery info mappings
            CreateMap<DeliveryInfo, DeliveryInfoDto>();
            CreateMap<DeliveryInfoCreateDto, DeliveryInfo>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => DeliveryStatus.Pending))
                .ForMember(dest => dest.EstimatedDeliveryDate,
                    opt => opt.MapFrom(src => DateTime.UtcNow.AddDays(5)));


            //customer
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //Notification
            CreateMap<Notification, NotificationDto>();
            CreateMap<CreateNotificationDto, Notification>();

            // Appointment mappings
            CreateMap<Appointment,AppointmentDto>();
            CreateMap<Appointment,AppointmentListDto>()
                .ForMember(d => d.CustomerName,
                    o => o.MapFrom(s => $"{s.Customer.FirstName} {s.Customer.LastName}"))
                .ForMember(d => d.ServiceName,
                    o => o.MapFrom(s => s.Service.Name))
                .ForMember(d => d.ServiceProviderName,
                    o => o.MapFrom(s => $"{s.ServiceProvider.FirstName} {s.ServiceProvider.LastName}"));
            CreateMap<CreateAppointmentDto,Appointment>();
            CreateMap<UpdateAppointmentDto,Appointment>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
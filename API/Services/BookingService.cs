﻿using API.Contracts;
using API.DTOs.Bookings;
using API.Models;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository,
            IEmployeeRepository employeeRepository,
            IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _roomRepository = roomRepository;
        }

        // GetAll Booking
        public IEnumerable<GetBookingDto>? GetBooking()
        {
            var bookings = _bookingRepository.GetAll();
            if (!bookings.Any())
            {
                return null; // No Booking  found
            }

            var toDto = bookings.Select(booking => new GetBookingDto

            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid
            }).ToList();

            return toDto; // Booking found
        }

        // GetByGuid Booking
        public GetBookingDto? GetBooking(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);
            if (booking is null)
            {
                return null; // booking not found
            }

            var toDto = new GetBookingDto
            {
                Guid = booking.Guid,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid
            };

            return toDto; // bookings found
        }

        // 
        public IEnumerable<DetailBookingDto> DetailBooking()
        {
            var bookings = _bookingRepository.GetAll();

            if (bookings == null)
            {
                return null;
            }

            var employees = _employeeRepository.GetAll();
            var rooms = _roomRepository.GetAll();

            var detailBookings = (from booking in bookings
                                  join employee in employees on booking.EmployeeGuid equals employee.Guid
                                  join room in rooms on booking.RoomGuid equals room.Guid
                                  where booking.StartDate <= DateTime.Now.Date && booking.EndDate >= DateTime.Now
                                  select new DetailBookingDto
                                  {
                                      BookingGuid = booking.Guid,
                                      RoomName = room.Name,
                                      Status = booking.Status,
                                      Floor = room.Floor,
                                      BookedBy = employee.FirstName + " " + employee.LastName,
                                  }).ToList();
            if (!detailBookings.Any())
            {
                return null;
            }

            return detailBookings;
        }

        // Create Booking
        public GetBookingDto? CreateBooking(NewBookingDto newBookingDto)
        {
            var booking = new Booking
            {
                Guid = new Guid(),
                StartDate = newBookingDto.StartDate,
                EndDate = newBookingDto.EndDate,
                Status = newBookingDto.Status,
                Remarks = newBookingDto.Remarks,
                RoomGuid = newBookingDto.RoomGuid,
                EmployeeGuid = newBookingDto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            var createdBooking = _bookingRepository.Create(booking);
            if (createdBooking is null)
            {
                return null; // Booking not created
            }

            var toDto = new GetBookingDto
            {
                Guid = createdBooking.Guid,
                StartDate = newBookingDto.StartDate,
                EndDate = newBookingDto.EndDate,
                Status = newBookingDto.Status,
                Remarks = newBookingDto.Remarks,
                RoomGuid = newBookingDto.RoomGuid,
                EmployeeGuid = newBookingDto.EmployeeGuid,
            };

            return toDto; // Booking created
        }

        public int UpdateBooking(GetBookingDto updateBookingDto)
        {
            var isExist = _bookingRepository.IsExist(updateBookingDto.Guid);
            if (!isExist)
            {
                return -1; // Booking not found
            }

            var getBooking = _bookingRepository.GetByGuid(updateBookingDto.Guid);

            var booking = new Booking
            {
                Guid = updateBookingDto.Guid,
                StartDate = updateBookingDto.StartDate,
                EndDate = updateBookingDto.EndDate,
                Status = updateBookingDto.Status,
                Remarks = updateBookingDto.Remarks,
                RoomGuid = updateBookingDto.RoomGuid,
                EmployeeGuid = updateBookingDto.EmployeeGuid,
                ModifiedDate = DateTime.Now,
                CreatedDate = getBooking!.CreatedDate
            };

            var isUpdate = _bookingRepository.Update(booking);
            if (!isUpdate)
            {
                return 0; // Booking not updated
            }

            return 1;
        }

        public int DeleteBooking(Guid guid)
        {
            var isExist = _bookingRepository.IsExist(guid);
            if (!isExist)
            {
                return -1; // Booking not found
            }

            var booking = _bookingRepository.GetByGuid(guid);
            var isDelete = _bookingRepository.Delete(booking!);
            if (!isDelete)
            {
                return 0; // Booking not deleted
            }

            return 1;
        }
    }
}

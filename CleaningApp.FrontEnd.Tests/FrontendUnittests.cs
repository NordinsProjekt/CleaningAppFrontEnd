using Bunit;
using CleaningApp.Application.Dtos;
using CleaningApp.Application.Services;
using CleaningApp.Domain.Entities;
using CleaningApp.Infrastructure.UnitOfWork;
using CleaningAppFrontEnd.Components.Dropdowns;
using CleaningAppFrontEnd.Components.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TaskStatus = CleaningApp.Domain.Entities.TaskStatus;

namespace CleaningApp.FrontEnd.Tests;

public class FrontendUnittests
{
    private IUnitOfWork _unitOfWorkMock;

    public FrontendUnittests()
    {
        // 1) Create the mock
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    }

    public class DayOfWeekDropdownTests : TestContext
    {
        [Fact]
        public void DayOfWeekDropdown_DefaultRendersSevenDayOptions()
        {
            // Arrange & Act
            var cut = RenderComponent<DayOfWeekDropdown>();

            // Assert: Verify that the <select> contains exactly 7 <option> elements for days 0..6
            var options = cut.FindAll("option");
            Assert.Equal(7, options.Count);
        }

        [Fact]
        public void DayOfWeekDropdown_WhenDayOfWeekValueProvided_SelectsThatOption()
        {
            // Arrange
            // Let's say we want it to be Wednesday (3) by default.
            var cut = RenderComponent<DayOfWeekDropdown>(parameters => parameters
                .Add(p => p.DayOfWeekValue, 3)
            );

            // Act
            var select = cut.Find("select");
            var selectedValue = select.GetAttribute("value");

            // Assert
            Assert.Equal("3", selectedValue); // Should reflect the bound value
        }

        [Fact]
        public void DayOfWeekDropdown_WhenValueChanged_CallsCallback()
        {
            // Arrange
            int? newValueFromCallback = null;
            var cut = RenderComponent<DayOfWeekDropdown>(parameters => parameters
                .Add(p => p.DayOfWeekValue, 0)
                .Add(p => p.DayOfWeekValueChanged,
                    EventCallback.Factory.Create<int>(this, v => newValueFromCallback = v))
            );

            // Act
            var select = cut.Find("select");
            select.Change("4"); // e.g. Thursday

            // Assert
            Assert.Equal(4, newValueFromCallback);
        }
    }

    public class RoomDropdownTests : TestContext
    {
        [Fact]
        public void RoomDropdown_WhenNoRoomsProvided_ShowsOnlyDefaultOption()
        {
            // Arrange & Act
            var cut = RenderComponent<RoomDropdown>();

            // Assert
            var options = cut.FindAll("option");
            // By default there's one “-- Select Room --” option if Rooms is empty
            Assert.Single(options);
            Assert.Equal("-- Select Room --", options[0].TextContent);
        }

        [Fact]
        public void RoomDropdown_ShowsAllRoomOptions()
        {
            // Arrange
            var sampleRooms = new List<RoomDto>
            {
                new RoomDto { Id = Guid.NewGuid(), Name = "Kitchen" },
                new RoomDto { Id = Guid.NewGuid(), Name = "Living Room" }
            };

            var cut = RenderComponent<RoomDropdown>(parameters => parameters
                .Add(p => p.Rooms, sampleRooms)
            );

            // Act
            var options = cut.FindAll("option");

            // Assert
            // The first option is “-- Select Room --”, plus 2 from sampleRooms => total 3
            Assert.Equal(3, options.Count);
            Assert.Equal("-- Select Room --", options[0].TextContent);
            Assert.Equal("Kitchen", options[1].TextContent);
            Assert.Equal("Living Room", options[2].TextContent);
        }

        [Fact]
        public void RoomDropdown_WhenSelectedRoomIdSet_IsReflectedInMarkup()
        {
            // Arrange
            var room1 = new RoomDto { Id = Guid.NewGuid(), Name = "Kitchen" };
            var room2 = new RoomDto { Id = Guid.NewGuid(), Name = "Living Room" };
            var sampleRooms = new List<RoomDto> { room1, room2 };

            var cut = RenderComponent<RoomDropdown>(parameters => parameters
                .Add(p => p.Rooms, sampleRooms)
                .Add(p => p.SelectedRoomId, room2.Id)
            );

            // Act
            var select = cut.Find("select");
            var selectedValue = select.GetAttribute("value");

            // Assert
            Assert.Equal(room2.Id.ToString(), selectedValue);
        }

        [Fact]
        public void RoomDropdown_WhenUserSelectsDifferentRoom_InvokesCallback()
        {
            // Arrange
            var roomIdCallback = Guid.Empty;
            var room1 = new RoomDto { Id = Guid.NewGuid(), Name = "Kitchen" };
            var room2 = new RoomDto { Id = Guid.NewGuid(), Name = "Living Room" };

            var cut = RenderComponent<RoomDropdown>(parameters => parameters
                .Add(p => p.Rooms, new List<RoomDto> { room1, room2 })
                .Add(p => p.SelectedRoomId, room1.Id)
                .Add(p => p.SelectedRoomIdChanged,
                    EventCallback.Factory.Create<Guid>(this, id => roomIdCallback = id))
            );

            // Act
            // Simulate user selecting the second room in the <select>
            var select = cut.Find("select");
            select.Change(room2.Id.ToString());

            // Assert
            Assert.Equal(room2.Id, roomIdCallback);
        }
    }

    public class TaskTypeDropdownTests : TestContext
    {
        [Fact]
        public void TaskTypeDropdown_ShowsAllTaskTypes()
        {
            // Arrange
            var types = new List<TaskTypeDto>
            {
                new TaskTypeDto { Id = Guid.NewGuid(), Name = "Mopping" },
                new TaskTypeDto { Id = Guid.NewGuid(), Name = "Dusting" }
            };

            // Act
            var cut = RenderComponent<TaskTypeDropdown>(parameters => parameters
                .Add(p => p.TaskTypes, types)
            );

            var options = cut.FindAll("option");

            // Assert
            // One “-- Select Task Type --”, plus 2 from the list => total 3
            Assert.Equal(3, options.Count);
            Assert.Equal("-- Select Task Type --", options[0].TextContent);
            Assert.Equal("Mopping", options[1].TextContent);
            Assert.Equal("Dusting", options[2].TextContent);
        }

        [Fact]
        public void TaskTypeDropdown_SetsSelectedValue()
        {
            // Arrange
            var typeId = Guid.NewGuid();
            var types = new List<TaskTypeDto>
            {
                new TaskTypeDto { Id = typeId, Name = "Vacuuming" }
            };

            // Act
            var cut = RenderComponent<TaskTypeDropdown>(parameters => parameters
                .Add(p => p.TaskTypes, types)
                .Add(p => p.SelectedTaskTypeId, typeId)
            );

            // Assert
            var select = cut.Find("select");
            Assert.Equal(typeId.ToString(), select.GetAttribute("value"));
        }

        [Fact]
        public void TaskTypeDropdown_WhenUserChangesValue_InvokesCallback()
        {
            // Arrange
            var selectedTypeId = Guid.Empty;
            var type1 = new TaskTypeDto { Id = Guid.NewGuid(), Name = "Wiping" };
            var type2 = new TaskTypeDto { Id = Guid.NewGuid(), Name = "Scrubbing" };

            var cut = RenderComponent<TaskTypeDropdown>(parameters => parameters
                .Add(p => p.TaskTypes, new List<TaskTypeDto> { type1, type2 })
                .Add(p => p.SelectedTaskTypeId, type1.Id)
                .Add(p => p.SelectedTaskTypeIdChanged,
                    EventCallback.Factory.Create<Guid>(this, val => selectedTypeId = val))
            );

            // Act
            var select = cut.Find("select");
            select.Change(type2.Id.ToString());

            // Assert
            Assert.Equal(type2.Id, selectedTypeId);
        }
    }

    public class UserDropdownTests : TestContext
    {
        [Fact]
        public void UserDropdown_WhenNoUsers_ShowsNoDefaultUserText()
        {
            // Arrange & Act
            var cut = RenderComponent<UserDropdown>();

            // Assert
            var options = cut.FindAll("option");
            // The first option is “-- No Default User --”
            Assert.Single(options);
            Assert.Equal("-- No Default User --", options[0].TextContent);
        }

        [Fact]
        public void UserDropdown_DisplaysAllUsers()
        {
            // Arrange
            var user1 = new UserDto { Id = Guid.NewGuid(), Name = "Alice" };
            var user2 = new UserDto { Id = Guid.NewGuid(), Name = "Bob" };
            var users = new List<UserDto> { user1, user2 };

            // Act
            var cut = RenderComponent<UserDropdown>(parameters => parameters
                .Add(p => p.Users, users)
            );

            var options = cut.FindAll("option");

            // Assert
            // 1st = “-- No Default User --”, 2 more => total 3
            Assert.Equal(3, options.Count);
            Assert.Equal("Alice", options[1].TextContent);
            Assert.Equal("Bob", options[2].TextContent);
        }

        [Fact]
        public void UserDropdown_SetsSelectedUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var users = new List<UserDto>
            {
                new UserDto { Id = userId, Name = "Tester" }
            };

            // Act
            var cut = RenderComponent<UserDropdown>(parameters => parameters
                .Add(p => p.Users, users)
                .Add(p => p.SelectedUserId, userId)
            );

            // Assert
            var select = cut.Find("select");
            Assert.Equal(userId.ToString(), select.GetAttribute("value"));
        }

        [Fact]
        public void UserDropdown_WhenChanged_InvokesCallback()
        {
            // Arrange
            var selectedUserId = Guid.Empty;
            var user1 = new UserDto { Id = Guid.NewGuid(), Name = "User1" };
            var user2 = new UserDto { Id = Guid.NewGuid(), Name = "User2" };

            var cut = RenderComponent<UserDropdown>(parameters => parameters
                .Add(p => p.Users, new List<UserDto> { user1, user2 })
                .Add(p => p.SelectedUserId, user1.Id)
                .Add(p => p.SelectedUserIdChanged,
                    EventCallback.Factory.Create<Guid>(this, id => selectedUserId = id))
            );

            // Act
            var select = cut.Find("select");
            select.Change(user2.Id.ToString());

            // Assert
            Assert.Equal(user2.Id, selectedUserId);
        }

        public class RegisterTaskComponentTests : TestContext
        {
            private readonly TaskService _taskServiceMock;
            private readonly IUnitOfWork _unitOfWorkMock;

            public RegisterTaskComponentTests()
            {
                // Create NSubstitute mocks
                _taskServiceMock = Substitute.For<TaskService>((IUnitOfWork)null!);
                _unitOfWorkMock = Substitute.For<IUnitOfWork>();

                // Register them in bUnit’s services, so that when 
                // the RegisterTask component injects TaskService & IUnitOfWork,
                // it will receive these mocks.
                Services.AddSingleton(_taskServiceMock);
                Services.AddSingleton(_unitOfWorkMock);
            }

            [Fact]
            public async Task RegisterTask_WhenInitialized_LoadsDropdownDataFromUnitOfWork()
            {
                // Arrange
                // Fake data
                var userId = Guid.NewGuid();
                var roomId = Guid.NewGuid();
                var taskTypeId = Guid.NewGuid();

                var mockUsers = new List<User>
                {
                    new User { Id = userId, Name = "TestUser" }
                };
                var mockRooms = new List<Room>
                {
                    new Room { Id = roomId, Name = "Living Room" }
                };
                var mockTaskTypes = new List<TaskType>
                {
                    new TaskType { Id = taskTypeId, Name = "Mopping" }
                };

                // Setup repositories
                var userRepo = Substitute.For<IRepository<User>>();
                userRepo.GetAllAsync().Returns(mockUsers);
                var roomRepo = Substitute.For<IRepository<Room>>();
                roomRepo.GetAllAsync().Returns(mockRooms);
                var taskTypeRepo = Substitute.For<IRepository<TaskType>>();
                taskTypeRepo.GetAllAsync().Returns(mockTaskTypes);

                // For the IUnitOfWork mock, return the above repositories based on T
                _unitOfWorkMock.Repository<User>().Returns(userRepo);
                _unitOfWorkMock.Repository<Room>().Returns(roomRepo);
                _unitOfWorkMock.Repository<TaskType>().Returns(taskTypeRepo);

                // Act
                // Render the RegisterTask component
                var cut = RenderComponent<RegisterTask>();

                // Wait for OnInitializedAsync to complete, so data is loaded
                cut.WaitForState(() =>
                    cut.FindAll("option").Count > 0
                );

                // Assert
                // Check that correct number of <option> tags show up 
                // across all three <select> elements (User, Room, TaskType).
                // Each <select> has 1 “placeholder” option + however many items in the list
                var selects = cut.FindAll("select");
                Assert.Equal(3, selects.Count);

                // 1) User <select>
                var userOptions = selects[0].QuerySelectorAll("option");
                Assert.Equal(2, userOptions.Count()); // "Select a User" + "TestUser"
                Assert.Contains(userOptions, opt => opt.TextContent == "TestUser");

                // 2) Room <select>
                var roomOptions = selects[1].QuerySelectorAll("option");
                Assert.Equal(2, roomOptions.Count()); // "Select a Room" + "Living Room"

                // 3) TaskType <select>
                var taskTypeOptions = selects[2].QuerySelectorAll("option");
                Assert.Equal(2, taskTypeOptions.Count()); // "Select a Task Type" + "Mopping"
            }
        }
    }
}
using SmartCampus.Application;
using SmartCampus.Domain.Enums;
using SmartCampus.Domain.Notifications;
using SmartCampus.Domain.Users;
using SmartCampus.Infrastructure.Configuration;
using SmartCampus.Infrastructure.Data;
using SmartCampus.Infrastructure.Logging;
using SmartCampus.Infrastructure.Notifications;

// ---- Composition root --------------------------------------------------------
// This is the only place that references concrete Infrastructure classes. It wires
// them to the abstractions; everything else depends on interfaces / Domain types.

Console.WriteLine($"=== {ConfigurationManager.Instance.SystemName} ===\n");

INotificationFactory notificationFactory = new NotificationFactory();   // Factory (2)
var userStore = new InMemoryUserStore();                                 // simple data
var publisher = new AnnouncementPublisher(Logger.Instance);             // Observer subject + Singleton logger
var service   = new AnnouncementService(publisher);                     // use case

// ---- Step 1 & 2: add users and set their notification preferences ------------
Console.WriteLine("Step 1-2  Register users and their preferred channels");
var ayse   = new Student("Ayse Yilmaz", "ayse@campus.edu",    NotificationChannel.Email, notificationFactory);
var mehmet = new Student("Mehmet Kaya",  "+90 555 111 22 33",  NotificationChannel.Sms,   notificationFactory);
var aydin  = new Teacher("Prof. Aydin",  "device-token-7781",  NotificationChannel.Push,  notificationFactory);

foreach (User u in new User[] { ayse, mehmet, aydin })
{
    userStore.Add(u);
    Console.WriteLine($"          + {u.Role,-7} {u.Name,-13} -> {u.PreferredChannel}");
}

// ---- Subscribe stored users to the publisher (Observer registration) ---------
Console.WriteLine("\nStep 3prep Subscribe users to the publisher (Observer pattern)");
foreach (User u in userStore.GetAll())
    publisher.Subscribe(u);

// ---- Step 3-5: admin creates an EXAM announcement -> Factory -> publish -------
// ---- Step 6-8: publishing notifies each observer, which builds its channel ----
//               via the Factory and prints the notification to the console.
Console.WriteLine("\nStep 3-8  Admin posts an EXAM announcement (Factory builds it, then it is published)");
var examData = new Dictionary<string, object>
{
    ["courseCode"]  = "BIL3204",
    ["newExamDate"] = new DateTime(2026, 6, 8, 10, 0, 0)
};
service.CreateAndPublish(AnnouncementType.Exam, examData);

// ---- Same pipeline, different announcement type, to prove it is generic ------
Console.WriteLine("\nBonus     A LIBRARY announcement through the very same pipeline");
var libraryData = new Dictionary<string, object>
{
    ["newHours"] = "08:00 - 24:00 during finals week"
};
service.CreateAndPublish(AnnouncementType.Library, libraryData);

// ---- Show Observer unsubscribe: Mehmet opts out, then we publish again -------
Console.WriteLine("\nBonus     Mehmet unsubscribes, then an EVENT is published");
publisher.Unsubscribe(mehmet);
var eventData = new Dictionary<string, object>
{
    ["title"]    = "Career Day 2026",
    ["location"] = "Main Auditorium",
    ["startsAt"] = new DateTime(2026, 6, 20, 13, 30, 0)
};
service.CreateAndPublish(AnnouncementType.Event, eventData);

Console.WriteLine("\n=== Scenario complete ===");

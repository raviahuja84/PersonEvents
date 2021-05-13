# Real Time PersonEvents updates
The application is a web application which could be used by adminstration staff in hospitals/clinics and doctors to get realtime updates for the patient 
events occuring in their premises.


**Technologies used**

- asp .net core
- sql server localdb (lightweight database housed in Visual Studio for debug)

**Nuget package dependencies**

- Asp .net Core 
- SignalR server (Hub) 
- SignalR Client
- SqlTableDependency
- EntityFrameworkCore
- EntityFrameworkCore.SqlServer

ASP.NET SignalR is a library for ASP.NET developers to add real-time web functionality to their applications. 
Real-time web functionality is the ability to have server-side code push content to the connected clients in real-time.
It is used commonly on duplex communications wherin supports the typical http(s) request/response model from client side initiation
as well server side sending broadcast/notifications to clients as push messages. It creates a persisted connection unerlying betwen server and clients
and leverages websockets as underlying technology.

The signalRHub needs some sort of trigger/event change to notify clients  (web browsers/desktop apps) in this case.
The trigger event source in this case is a database table (Events) used as datastore for events related to a patient.
The signalR hub subscribes to the sql table ('Events') in this case for any DML operation (insert, update, delete) and then it would 
join the data based on the events belonging to which patient and then preparing the real time /on the fly person with list of events being relayed back
to the clients (browsers).

**Assumptions**

The Person and Events (related to the person) datastores could be anywhere in azure sql/storage /No sql database.
For simplicity we have have leveraged on localdb (lightweight) VS database.
On every event arriving to the datastore, the clients (dcotors) to get updated event information for the person.

**Typical Scenario**

The patient administration dept. (could be reception desk at hospitals/clinics) which on patient arrival
would update patient information (assume new patient walks in OR to validate patients personal information is upto date on hospital records).

Now, every visit for the patient could be treated as an 'Event' in persons life which has brought him to the clinics/hospital.
So the real time updates to person and event store is done by front desk and the doctors could see the real tiem updates.

Assume doctors machine/browsers are clients subscried to real time updates from sigNalR updates.


**Questions** (As part of the challenge/Exercise)

1.	If you had more time, what would you change or focus more time on?
    - Create a nice front end UI for clients to receive and storing events.
    - Code review once again to see if design can be made better
    - Re visit code for code performance improvement with respect to the backend response time.
    - Provide handling for specific doctor push/notification as a single client or relevant client push instead of boadcast to all doctors at the moment.

2.	Which part of the solution consumed the most amount of time?
    - Wiring the signalR Hub and SqlTableDependeies together on the asp .net core backend.
    - Fixing incompatible build time issues on asp. net core 3.1 with respect to CLR api calls.

3.	What would you suggest to the clinicians that they may not have thought of in regard to their request?
    - Provide single client push from backend (signalR hub) would be nice feature and not pollute with notification to other doctors the patient may not be relevant.
    - Suggest them to use more unique person identifiers in health eco system.
      This would help stirae and normalise data on the backend in a much cleaner way.
      Some identifiers in mind could be medicare card no or Individual health idnetifier (IHI) which are national identifiers for a person.


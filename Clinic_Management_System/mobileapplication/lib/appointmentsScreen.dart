import 'package:flutter/material.dart';

import 'appointmentDetails.dart';
import 'data/AppointmentService.dart';

class AppointmentsScreen extends StatefulWidget {
  const AppointmentsScreen({super.key});

  @override
  State<AppointmentsScreen> createState() => _AppointmentsScreenState();
}

class _AppointmentsScreenState extends State<AppointmentsScreen> {

  final AppointmentService appointmentService = AppointmentService();

  late Future<List<dynamic>> appointments;

  @override
  void initState() {
    super.initState();
    appointments = appointmentService.getAppointments();
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Appointments'),
        backgroundColor: Colors.indigo,
      ),

      body: FutureBuilder<List<dynamic>>(
        future: appointments,

        builder: (context, snapshot) {

          if (snapshot.connectionState == ConnectionState.waiting) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (snapshot.hasError) {
            return Center(
              child: Text('Error: ${snapshot.error}'),
            );
          }

          final appointmentsList = snapshot.data ?? [];

          if (appointmentsList.isEmpty) {
            return const Center(
              child: Text('No Appointments Found'),
            );
          }

          return GridView.builder(

            padding: const EdgeInsets.all(12),

            gridDelegate:
            const SliverGridDelegateWithFixedCrossAxisCount(
              crossAxisCount: 2,
              crossAxisSpacing: 10,
              mainAxisSpacing: 10,
              childAspectRatio: 0.75,
            ),

            itemCount: appointmentsList.length,

            itemBuilder: (context, index) {

              final appointment = appointmentsList[index];

              return InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) =>
                          AppointmentDetails(appointment),
                    ),
                  );
                },

                child: Card(
                  elevation: 4,

                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(15),
                  ),

                  child: Column(
                    children: [

                      Expanded(
                        child: Center(
                          child: Icon(
                            Icons.calendar_month,
                            size: 60,
                            color: Colors.indigo,
                          ),
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Patient Name
                      Text(
                        appointment['patientName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 16,
                          fontWeight: FontWeight.bold,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),

                      const SizedBox(height: 5),

                      // Doctor Name
                      Text(
                        appointment['doctorName']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),

                      // Appointment Date
                      Text(
                        appointment['appointmentDate']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 14,
                        ),
                        maxLines: 1,
                        overflow: TextOverflow.ellipsis,
                      ),

                      const SizedBox(height: 10),

                      // Status
                      Text(
                        appointment['status']?.toString() ?? '',
                        style: const TextStyle(
                          fontSize: 15,
                          color: Colors.indigo,
                          fontWeight: FontWeight.bold,
                        ),
                      ),

                      const SizedBox(height: 10),
                    ],
                  ),
                ),
              );
            },
          );
        },
      ),
    );
  }
}
import 'package:flutter/material.dart';

class AppointmentDetails extends StatelessWidget {

  final dynamic appointment;

  const AppointmentDetails(this.appointment, {super.key});

  @override
  Widget build(BuildContext context) {

    return Scaffold(

      appBar: AppBar(
        title: const Text('Appointment Details'),
        backgroundColor: Colors.indigo,
      ),

      body: SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,

          children: [

            // Appointment Icon
            SizedBox(
              width: double.infinity,
              height: 300,

              child: const Icon(
                Icons.calendar_month,
                size: 100,
              ),
            ),

            const SizedBox(height: 20),

            Padding(
              padding: const EdgeInsets.all(16),

              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,

                children: [

                  // Patient Name
                  Text(
                    appointment['patientName']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 26,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Doctor Name
                  Text(
                    appointment['doctorName']?.toString() ?? '',
                    style: const TextStyle(
                      fontSize: 24,
                      color: Colors.green,
                      fontWeight: FontWeight.bold,
                    ),
                  ),

                  const SizedBox(height: 20),

                  const Divider(),

                  const SizedBox(height: 15),

                  // Appointment Date
                  Text(
                    'Appointment Date: ${appointment['appointmentDate'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Status
                  Text(
                    'Status: ${appointment['status'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),

                  const SizedBox(height: 15),

                  // Reason
                  Text(
                    'Reason: ${appointment['reason'] ?? 'N/A'}',
                    style: const TextStyle(
                      fontSize: 18,
                    ),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
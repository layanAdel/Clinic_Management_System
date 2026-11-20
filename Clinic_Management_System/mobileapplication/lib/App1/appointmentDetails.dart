import 'package:flutter/material.dart';

import '../App1/data/CartService.dart';


class AppointmentDetails extends StatelessWidget {
  final dynamic appointment;
  final int patientId;

  const AppointmentDetails(this.appointment, this.patientId, {super.key});


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

                  ElevatedButton.icon(
                    onPressed: () async {
                      try {
                        await Cartservice().addToCart(
                          patientId,
                          appointment['id'],
                        );

                        if (!context.mounted) return;

                        ScaffoldMessenger.of(context).showSnackBar(
                          const SnackBar(
                            content: Text('Appointment Added to Cart'),
                          ),
                        );
                      } catch (e) {
                        if (!context.mounted) return;

                        ScaffoldMessenger.of(context).showSnackBar(
                          SnackBar(content: Text('Error: $e')),
                        );
                      }
                    },
                    icon: const Icon(Icons.shopping_cart),
                    label: const Text('Add to Cart'),
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
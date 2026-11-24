import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../provider/CartProvider.dart';
import '../provider/UserProvider.dart';



class AppointmentDetails extends StatelessWidget {
  final dynamic appointment;

  const AppointmentDetails(this.appointment ,{super.key});


  @override
  Widget build(BuildContext context) {
  final userProvider = context.watch<Userprovider>();

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

                      final cartProvider = context.read<CartProvider>();

                      final success = await cartProvider.addToCart(
                        userProvider.patientId,
                        appointment['id'],
                      );

                      if (!context.mounted) return;

                      if (success) {
                        ScaffoldMessenger.of(context).showSnackBar(
                          const SnackBar(
                            content: Text('Appointment Added to Cart'),
                          ),
                        );
                      } else {
                        ScaffoldMessenger.of(context).showSnackBar(
                          SnackBar(
                            content: Text(
                              cartProvider.error ?? 'Failed to add appointment',
                            ),
                          ),
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
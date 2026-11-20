import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../provider/OrderProvider.dart';
import '../provider/UserProvider.dart';

class MyOrdersScreen extends StatefulWidget {

  const MyOrdersScreen({super.key});

  @override
  State<MyOrdersScreen> createState() => _MyOrdersScreenState();
}

class _MyOrdersScreenState extends State<MyOrdersScreen> {


  @override
  void initState() {
    super.initState();

    Future.microtask(() {
      if (!mounted) return;

      final patientId = context.read<Userprovider>().patientId;

      context.read<OrderProvider>().getMyOrders(patientId);
    });
  }


  Future<void> refreshOrders() async {
    final patientId = context.read<Userprovider>().patientId;

    await context.read<OrderProvider>().getMyOrders(patientId);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('My Bookings'),
        backgroundColor: Colors.indigo,
      ),


      body: Consumer<OrderProvider>(
        builder: (context, provider, child) {

          if (provider.isLoading) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (provider.error != null) {
            return Center(
              child: Text('Error: ${provider.error}'),
            );
          }

          final items = provider.orders;

          if (items.isEmpty) {
            return const Center(
              child: Text('No Bookings Found'),
            );
          }

          return RefreshIndicator(
            onRefresh: refreshOrders,
            child: ListView.builder(
              itemCount: items.length,
              itemBuilder: (context, index) {
                final order = items[index];
                final appointments = order['items'] as List? ?? [];

                return Card(
                  margin: const EdgeInsets.all(10),
                  child: Padding(
                    padding: const EdgeInsets.all(15),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [

                        Text(
                          'Booking #${order['id']}',
                          style: const TextStyle(
                            fontSize: 20,
                            fontWeight: FontWeight.bold,
                          ),
                        ),

                        const SizedBox(height: 10),

                        Text(
                          'Date: ${order['createdAt']}',
                        ),

                        Text(
                          'Status: ${order['status']}',
                        ),

                        const Divider(),

                        const Text(
                          'Appointments:',
                          style: TextStyle(
                            fontWeight: FontWeight.bold,
                          ),
                        ),

                        ...appointments.map((appointment) {
                          return ListTile(
                            title: Text(
                              'Appointment #${appointment['appointmentId']}',
                            ),
                          );
                        }),

                      ],
                    ),
                  ),
                );
              },
            ),
          );
        },
      ),
    );
  }
}
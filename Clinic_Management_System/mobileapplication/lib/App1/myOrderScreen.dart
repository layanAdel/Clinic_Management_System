import 'package:flutter/material.dart';
import 'data/OrderService.dart';

class MyOrdersScreen extends StatefulWidget {
  final int patientId;

  const MyOrdersScreen(this.patientId, {super.key});

  @override
  State<MyOrdersScreen> createState() => _MyOrdersScreenState();
}

class _MyOrdersScreenState extends State<MyOrdersScreen> {
  final OrderService orderService = OrderService();

  late Future<List<dynamic>> orders;

  @override
  void initState() {
    super.initState();
    loadOrders();
  }

  void loadOrders() {
    orders = orderService.getMyOrders(widget.patientId);
  }

  Future<void> refreshOrders() async {
    setState(() {
      loadOrders();
    });

    await orders;
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('My Bookings'),
        backgroundColor: Colors.indigo,
      ),
      body: FutureBuilder<List<dynamic>>(
        future: orders,
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

          final items = snapshot.data ?? [];

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
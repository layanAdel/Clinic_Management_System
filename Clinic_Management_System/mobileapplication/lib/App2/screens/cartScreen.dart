    import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../provider/CartProvider.dart';
import '../provider/OrderProvider.dart';
import '../provider/UserProvider.dart';

    class CartScreen extends StatefulWidget {
    const CartScreen({super.key});

    @override
    State<CartScreen> createState() => _CartScreenState();
    }

    class _CartScreenState extends State<CartScreen> {


    late final provider = context.read<Userprovider>();


    bool isOrdering = false;

    Future<void> makeOrder() async {
        if (isOrdering) return;

        setState(() {
            isOrdering = true;
        });

        try {
            final orderProvider = context.read<OrderProvider>();

            final orderId = await orderProvider.makeOrder(provider.patientId);

            if (!mounted) return;

            if (orderId != null) {
                loadCart();

                ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(
                        content: Text('Booking #$orderId created successfully'),
                        backgroundColor: Colors.green,
                    ),
                );
            } else {
                ScaffoldMessenger.of(context).showSnackBar(
                    SnackBar(
                        content: Text(
                            orderProvider.error ?? 'Failed to create order',
                        ),
                        backgroundColor: Colors.red,
                    ),
                );
            }
        } catch (e) {
            if (!mounted) return;

            ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                    content: Text('Error: $e'),
                    backgroundColor: Colors.red,
                ),
            );
        } finally {
            if (mounted) {
                setState(() {
                    isOrdering = false;
                });
            }
        }
    }

    void loadCart() {
        context.read<CartProvider>().getCart(provider.patientId);
    }

    Future<void> deleteAppointment(int appointmentId) async {

        final cartProvider = context.read<CartProvider>();

        final success = await cartProvider.removeFromCart(
            provider.patientId,
            appointmentId,
        );

        if (!mounted) return;

        if (success) {
            ScaffoldMessenger.of(context).showSnackBar(
                const SnackBar(
                    content: Text('Appointment removed from cart'),
                ),
            );
        } else {
            ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                    content: Text(
                        cartProvider.error ?? 'Failed to remove appointment',
                    ),
                ),
            );
        }
    }

    @override
    void initState() {
        super.initState();

        Future.microtask(() {
            if (!mounted) return;
            loadCart();
        });
    }

    @override
    Widget build(BuildContext context) {
    return Scaffold(
    appBar: AppBar(
    title: const Text('My Cart'),
    backgroundColor: Colors.indigo,
    ),

        body: Consumer<CartProvider>(
            builder: (context, cartProvider, child) {

                if (cartProvider.isLoading) {
                    return const Center(
                        child: CircularProgressIndicator(),
                    );
                }

                if (cartProvider.error != null) {
                    return Center(
                        child: Text(cartProvider.error!),
                    );
                }

                final items = cartProvider.cart;

    if (items.isEmpty) {
    return const Center(
    child: Text('Your Cart is Empty'),
    );
    }

    return Column(
    children: [
    Expanded(
    child: ListView.builder(
    itemCount: items.length,
    itemBuilder: (context, index) {
    final item = items[index];

    return Card(
    margin: const EdgeInsets.all(10),
    child: ListTile(
    leading: const SizedBox(
    width: 65,
    height: 65,
    child: Icon(
    Icons.calendar_month,
    size: 40,
    color: Colors.indigo,
    ),
    ),

    title: Text(
    item['doctorName']?.toString() ?? '',
    ),

    subtitle: Text(
    'Date: ${item['appointmentDate']}\n'
    'Status: ${item['status']}',
    ),

    isThreeLine: true,

    trailing: IconButton(
    icon: const Icon(
    Icons.delete,
    color: Colors.red,
    ),
    onPressed: () {
    deleteAppointment(item['appointmentId']);
    },
    ),
    ),
    );
    },
    ),
    ),

    Padding(
    padding: const EdgeInsets.all(16),
    child: SizedBox(
    width: double.infinity,
    height: 50,
    child: ElevatedButton(
    onPressed: isOrdering ? null : makeOrder,
    style: ElevatedButton.styleFrom(
    backgroundColor: Colors.indigo,
    foregroundColor: Colors.white,
    ),
    child: isOrdering
    ? const CircularProgressIndicator(
    color: Colors.white,
    )
        : const Text(
    'Confirm Booking',
    style: TextStyle(fontSize: 18),
    ),
    ),
    ),
    ),
    ],
    );
    },
    ),
    );
    }
    }
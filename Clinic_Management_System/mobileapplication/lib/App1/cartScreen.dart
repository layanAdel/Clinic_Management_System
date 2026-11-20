    import 'package:flutter/material.dart';

    import 'data/CartService.dart';
    import 'data/orderService.dart';

    class CartScreen extends StatefulWidget {
    const CartScreen(this.patientId, {super.key});
    final int patientId;

    @override
    State<CartScreen> createState() => _CartScreenState();
    }

    class _CartScreenState extends State<CartScreen> {

    final Cartservice cartService = Cartservice();

    late Future<List<dynamic>> cartItems;

    final OrderService orderService = OrderService();
    bool isOrdering = false;

    Future<void> makeOrder() async {
    if (isOrdering) return;

    setState(() {
    isOrdering = true;
    });

        try {
        final orderId = await orderService.makeOrder(widget.patientId);

        if (!mounted) return;

        setState(() {
        loadCart();
        });

        ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
        content: Text('Booking #$orderId created successfully'),
        backgroundColor: Colors.green,
        ),
        );

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
    cartItems = cartService.getCart(widget.patientId);
    }

    Future<void> deleteAppointment(int appointmentId) async {
    try {
    await cartService.removeFromCart(
    widget.patientId,
    appointmentId,
    );

    if (!mounted) return;

    setState(() {
    loadCart();
    });

    ScaffoldMessenger.of(context).showSnackBar(
    const SnackBar(
    content: Text('Appointment removed from cart'),
    ),
    );
    } catch (e) {
    if (!mounted) return;

    ScaffoldMessenger.of(context).showSnackBar(
    SnackBar(content: Text('Error: $e')),
    );
    }
    }

    @override
    void initState() {
    super.initState();
    loadCart();
    }

    @override
    Widget build(BuildContext context) {
    return Scaffold(
    appBar: AppBar(
    title: const Text('My Cart'),
    backgroundColor: Colors.indigo,
    ),

    body: FutureBuilder<List<dynamic>>(
    future: cartItems,
    builder: (context, snapshot) {
    if (snapshot.connectionState ==
    ConnectionState.waiting) {
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
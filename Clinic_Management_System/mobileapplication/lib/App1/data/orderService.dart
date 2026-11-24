import 'dart:convert';
import 'package:http/http.dart' as http;
import 'conniction.dart';

class OrderService {

  // Make Order
  Future<int> makeOrder(int patientId) async {
    final response = await http.post(
      Uri.parse('${ConectionString.orders}/$patientId'),
    );

    if (response.statusCode == 200) {
      final data = jsonDecode(response.body);
      return (data['orderId'] as num).toInt();
    }

    throw Exception('Failed to create order: ${response.body}');
  }

  // Get My Orders
  Future<List<dynamic>> getMyOrders(int patientId) async {
    final response = await http.get(
      Uri.parse('${ConectionString.orders}/patient/$patientId'),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body) as List<dynamic>;
    }

    throw Exception('Failed to load orders');
  }
}
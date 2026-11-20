import 'dart:convert';

import 'package:http/http.dart' as http;

import 'conniction.dart';

class Cartservice {

  Future<List<dynamic>> getCart(int patientId) async {
    final response = await http.get(
      Uri.parse('${ConectionString.carts}/$patientId'),
    );

    if (response.statusCode == 200) {
      return jsonDecode(response.body);
    }

    throw Exception('Failed to load cart');
  }

  Future<void> addToCart(int patientId, int appointmentId) async {
    final response = await http.post(
      Uri.parse(ConectionString.carts),
      headers: {'Content-Type': 'application/json'},
      body: jsonEncode({
        'patientId': patientId,
        'appointmentId': appointmentId,
      }),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to add appointment');
    }
  }

  Future<void> removeFromCart(
      int patientId,
      int appointmentId,
      ) async {
    final response = await http.delete(
      Uri.parse('${ConectionString.carts}/$patientId/$appointmentId'),
    );

    if (response.statusCode != 200) {
      throw Exception('Failed to remove appointment');
    }
  }

}
import 'dart:convert';

import 'package:http/http.dart' as http;

import 'conniction.dart';

class AppointmentService {

  Future<List<dynamic>> getAppointments() async {

    final response = await http.get(
      Uri.parse(ConectionString.appointments),
    );

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('Failed to load Appointments');
    }
  }
}
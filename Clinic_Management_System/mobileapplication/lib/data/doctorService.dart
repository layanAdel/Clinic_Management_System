import 'dart:convert';

import 'package:http/http.dart' as http;

import 'conniction.dart';

class DoctorService {


  Future<List<dynamic>> getDoctors() async {

    final response = await http.get(
  Uri.parse(ConectionString.doctors),
  );

  if (response.statusCode == 200) {
  return json.decode(response.body);
  } else {
  throw Exception('Failed to load doctors');
  }
}


}
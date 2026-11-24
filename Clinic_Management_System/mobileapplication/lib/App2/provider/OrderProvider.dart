import 'package:flutter/material.dart';
import '../data/OrderService.dart';

class OrderProvider extends ChangeNotifier {

  final OrderService orderService = OrderService();

  List<dynamic> orders = [];

  bool isLoading = false;
  String? error;

  Future<void> getMyOrders(int patientId) async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      orders = await orderService.getMyOrders(patientId);
    } catch (e) {
      error = 'Failed to load orders';
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }

  Future<int?> makeOrder(int patientId) async {
    isLoading = true;
    error = null;
    notifyListeners();

    try {
      final orderId = await orderService.makeOrder(patientId);
      await getMyOrders(patientId);
      return orderId;
    } catch (e) {
      error = 'Failed to create order';
      return null;
    } finally {
      isLoading = false;
      notifyListeners();
    }
  }
}
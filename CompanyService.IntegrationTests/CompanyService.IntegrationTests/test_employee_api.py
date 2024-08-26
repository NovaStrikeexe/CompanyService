import requests

BASE_URL = "http://localhost:8080/api/Employees"

def test_get_all_employees():
    response = requests.get(BASE_URL)
    assert response.status_code == 200
    data = response.json()
    assert isinstance(data, list)

def test_get_employee_by_id():
    response = requests.get(f"{BASE_URL}/1")
    assert response.status_code == 200
    data = response.json()
    assert "id" in data
    assert data["id"] == 1

def test_add_employee():
    new_employee = {"firstName": "Test", "lastName": "Employee", "departmentId": 1}
    response = requests.post(BASE_URL, json=new_employee)
    assert response.status_code == 201
    data = response.json()
    assert data["firstName"] == "Test"
    assert "id" in data

def test_update_employee():
    updated_employee = {"id": 1, "firstName": "Updated", "lastName": "Employee", "departmentId": 1}
    response = requests.put(f"{BASE_URL}/1", json=updated_employee)
    assert response.status_code == 204

def test_delete_employee():
    response = requests.delete(f"{BASE_URL}/1")
    assert response.status_code == 204

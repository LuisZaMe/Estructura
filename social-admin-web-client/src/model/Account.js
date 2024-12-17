class Account {
    static ADMIN = 1;
    static INTERVIEWER = 2;
    static ANALYST = 3;
    static CLIENT = 4;
    static SUPER_ADMINISTRADOR = 5;

    constructor(email, name, lastname, role, phone, company, stateId, cityId) {
        this.email = btoa(email)
        this.password = btoa("12345")
        this.name = name
        this.lastname = lastname
        this.role = role
        this.phone = phone
        this.companyInformation = company
        this.stateId = stateId
        this.cityId = cityId
    }
}

export default Account
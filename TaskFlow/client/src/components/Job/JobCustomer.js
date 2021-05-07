import { Card, CardBody, CardHeader, CardText } from "reactstrap";

const JobCustomer = ({ customer }) => {
  return (
    <Card className="m-4">
      <CardHeader>
        <strong>{customer.name}</strong>
        <strong>{customer.phoneNumber}</strong>
      </CardHeader>
    </Card>
  );
};

export default JobCustomer;

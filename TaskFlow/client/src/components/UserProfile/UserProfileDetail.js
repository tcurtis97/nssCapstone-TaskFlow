import React, { useContext, useEffect, useState } from "react";
import { useHistory, useParams } from "react-router";
import { Button, Card, CardBody, CardFooter, Col, Row } from "reactstrap";
import { UserProfileContext } from "../../providers/UserProfileProvider";

const UserProfileDetails = () => {
  const { getUserProfileById } = useContext(UserProfileContext);
  const [profile, setProfile] = useState([]);

  const history = useHistory();

  useEffect(() => {
    getUserProfileById().then(setProfile);
  }, []);

  return (
    <div className="container">
      <Card>
        <CardBody>
          <Row>
            <Col>
              <h2>{profile.displayName}</h2>
              <h5>{profile.fullName}</h5>
              <br />
              <Row>
                <Col lg="6">
                  <h5>Email:</h5>
                  <p>{profile.email}</p>
                </Col>
                <Col md="6" lg="4">
                  <h5>Account Created:</h5>
                  <p>
                    {
                      new Date(profile.createDateTime)
                        .toLocaleString("en-US")
                        .split(", ")[0]
                    }
                  </p>
                </Col>
              </Row>
            </Col>
          </Row>
        </CardBody>
        <CardFooter>
          <Row>
            <Col>
              <div style={{ float: "right" }}></div>
            </Col>
          </Row>
        </CardFooter>
      </Card>
    </div>
  );
};

export default UserProfileDetails;

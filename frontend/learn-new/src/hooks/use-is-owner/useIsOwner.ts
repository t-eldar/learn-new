import { useState, useEffect } from "react";
import { useAuthentication } from "../use-authentication";

export const useIsOwner = (resourse: { userId: string } | undefined) => {
  const [isOwner, setIsOwner] = useState(false);

  const { authUser } = useAuthentication() ?? { authUser: undefined };

  useEffect(() => {
    if (typeof resourse === "undefined" || typeof authUser === "undefined") {
      return;
    }

    if (resourse.userId === authUser.id) {
      setIsOwner(true);
    } else {
      setIsOwner(false);
    }
  }, [resourse, authUser]);

  return { isOwner };
};

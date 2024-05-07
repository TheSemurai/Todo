import React, { useContext } from "react";

export default function BasePopup(props) {
  const closePopup = (event) => {
    if (event.currentTarget != event.target) return;
    props.setTrigger(!props.trigger);
  };

  return props.trigger ? (
    <div className="popup" onClick={closePopup}>
      <div className="popup-inner" onClick={closePopup}>
        <div className="popupclose"></div>
        {props.children}
      </div>
    </div>
  ) : (
    ""
  );
}

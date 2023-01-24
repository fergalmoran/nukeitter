import React from "react";

const NavbarComponent = () => {
  return (
    <div className="navbar bg-base-100">
      <div className="flex-none">
        <img
          src="/img/logo.svg"
          alt="Radioactive"
          className="w-8 h-8 mx-auto text-gray-900 fill-current"
        />
      </div>
      <div className="flex-1">
        <a className="text-xl normal-case btn btn-ghost">NukeTwitter</a>
      </div>
      <div className="flex-none">
        <span>Stuff on the right</span>
      </div>
    </div>
  );
};

export default NavbarComponent;

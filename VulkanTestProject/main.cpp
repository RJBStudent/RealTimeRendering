#include <windows.h>
#include <stdio.h>
#include  <vulkan/vulkan.h>
#include  <vulkan/vulkan.hpp>

#include <iostream>

VkInstance AppInstance;
VkInstanceCreateInfo InstanceCreateInfo;
VkApplicationInfo AppInfo;


int main()
{


    /*
    uint32_t extensions = 0;
    vkEnumerateInstanceExtensionProperties(nullptr, &extensions, nullptr);
    VkExtensionProperties* extensionPros = new VkExtensionProperties[extensions];
    vkEnumerateInstanceExtensionProperties(nullptr, &extensions, extensionPros);
    
    AppInfo.sType = VK_STRUCTURE_TYPE_APPLICATION_INFO;
    AppInfo.pNext = nullptr;
    AppInfo.pApplicationName = "Vulkan!!!!";
    AppInfo.pEngineName = "VulkanEngine";
    AppInfo.engineVersion = 1;
    AppInfo.applicationVersion = 1;
    AppInfo.apiVersion = VK_API_VERSION_1_0;

    InstanceCreateInfo.sType = VK_STRUCTURE_TYPE_INSTANCE_CREATE_INFO;
    InstanceCreateInfo.pNext = nullptr;
    InstanceCreateInfo.enabledLayerCount = 0;
    InstanceCreateInfo.enabledExtensionCount = 0;
    InstanceCreateInfo.flags = 0;
    InstanceCreateInfo.ppEnabledExtensionNames = nullptr;
    InstanceCreateInfo.ppEnabledLayerNames = nullptr;

    InstanceCreateInfo.pApplicationInfo = &AppInfo;
    VkResult success = vkCreateInstance(&InstanceCreateInfo, nullptr, &AppInstance);

    */
/*
*/
    auto extension = vk::enumerateInstanceExtensionProperties();



    const char* requiredExtensions[] = { "VK_KHR_surface", "VK_KHR_win32_surface" };
    //HACKKY WAY
    auto info = vk::InstanceCreateInfo().setPpEnabledExtensionNames(requiredExtensions);
    //info.setPpEnabledExtensionNames(extension);   //NOT HACKY WAY
    //info.setPpEnabledExtensionNames(requiredExtensions);

    auto appInstance = vk::createInstanceUnique(info);

    if (&appInstance != nullptr )
    {
        std::cout << "VULKAN IS UP AND RUNNING";
    }
    else
    {
        std::cout << "VULKAN ISNT WORKING...";
    }

    std::cin.get();
}

